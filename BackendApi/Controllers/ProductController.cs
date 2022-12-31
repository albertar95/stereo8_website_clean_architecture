using Application.DTO.Product;
using Application.Feature.Product.Requests;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //product section
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            return Ok(await _mediator.Send(new CreateProductRequest() { product = productDto }));
        }
        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto productDto)
        {
            return Ok(await _mediator.Send(new UpdateProductRequest() { product = productDto }));
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductRequest() { Id = id }));
        }
        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(Guid id) 
        {
            return Ok(await _mediator.Send(new GetProductRequest() {  Id = id}));
        }
        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList([FromQuery] int state = 0)
        {
            return Ok(await _mediator.Send(new GetProductListRequest() { State = state }));
        }
        [HttpPost("GetFilteredProduct")]
        public async Task<IActionResult> GetProduct([FromBody] ProductFilters filter)
        {
            return Ok(await _mediator.Send(new GetFilteredProductsRequest() { Filters = filter }));
        }
        [HttpGet("GetProductDetail/{id}")]
        public async Task<IActionResult> GetProductDetail(Guid id) 
        {
            return Ok(await _mediator.Send(new GetProductDetailRequest() {  Id = id }));
        }
    }
}
