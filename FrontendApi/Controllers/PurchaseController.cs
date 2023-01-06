using Application.DTO.Cart;
using Application.DTO.Favorite;
using Application.Feature.Cart.Requests;
using Application.Feature.Favorite.Requests;
using Application.Feature.Product.Requests;
using Application.Feature.ShipPrice.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetCarts/{userId}")]
        public async Task<IActionResult> GetCarts(Guid userId)
        {
            return Ok(await _mediator.Send(new CartListRequest() { UserId = userId }));
        }
        [HttpGet("GetFavorites/{userId}")]
        public async Task<IActionResult> GetFavorites(Guid userId)
        {
            return Ok(await _mediator.Send(new FavoriteListRequest() { UserId = userId }));
        }
        [HttpGet("GetCartCountByUserId/{userId}")]
        public async Task<IActionResult> GetCartCountByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new CartCountByUserIdRequest() { UserId = userId }));
        }
        [HttpPost("CreateCart")]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartDto cart)
        {
            return Ok(await _mediator.Send(new CreateCartRequest() { Cart = cart }));
        }
        [HttpGet("GetUsersCartByProductId/{userId}/{productId}")]
        public async Task<IActionResult> GetUsersCartByProductId(Guid userId,Guid productId)
        {
            return Ok(await _mediator.Send(new GetCartByProductId() { UserId = userId, ProductId = productId }));
        }
        [HttpPatch("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartDto cart)
        {
            return Ok(await _mediator.Send(new UpdateCartRequest() { Cart = cart }));
        }
        [HttpDelete("DeleteCart/{id}")]
        public async Task<IActionResult> DeleteCart(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCartRequest() { Id = id }));
        }
        [HttpGet("GetCartAggregateByUserId/{userId}")]
        public async Task<IActionResult> GetCartAggregateByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new CartAggregateByUserIdRequest() { UserId = userId }));
        }
        [HttpGet("GetCart/{id}")]
        public async Task<IActionResult> GetCart(Guid id)
        {
            return Ok(await _mediator.Send(new GetCartRequest() { Id = id }));
        }
        [HttpGet("GetShipPrices")]
        public async Task<IActionResult> GetShipPrices()
        {
            return Ok(await _mediator.Send(new ShipPriceListRequest()));
        }
        [HttpPost("CreateFavorite")]
        public async Task<IActionResult> CreateFavorite([FromBody] CreateFavoriteDto fav)
        {
            return Ok(await _mediator.Send(new CreateFavoriteRequest() { Favorite = fav }));
        }
        [HttpDelete("DeleteFavorite/{id}")]
        public async Task<IActionResult> DeleteFavorite(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteFavoriteRequest() { Id = id }));
        }
        [HttpGet("GetFavoriteCountByUserId/{userId}")]
        public async Task<IActionResult> GetFavoriteCountByUserId(Guid userId)
        {
            return Ok(await _mediator.Send(new FavoriteCountRequest() { UserId = userId }));
        }
    }
}
