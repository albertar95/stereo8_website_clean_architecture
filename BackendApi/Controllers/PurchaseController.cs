using Application.Feature.Order.Requests;
using Application.Feature.Ship.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
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
        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetOrders([FromQuery] int state = 0, [FromQuery] int pageSize = 100,int skip = 0) 
        {
            return Ok(await _mediator.Send(new OrderListRequest() {  State = state, Pagesize = pageSize, Skip = skip }));
        }
        [HttpGet("GetOrder/{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            return Ok(await _mediator.Send(new GetOrderRequest() { Id = id }));
        }
        [HttpGet("GetShips")]
        public async Task<IActionResult> GetShips([FromQuery] int state = 0, [FromQuery] int pageSize = 100, int skip = 0)
        {
            return Ok(await _mediator.Send(new ShipListRequest() { State = state, Pagesize = pageSize, Skip = skip }));
        }
        [HttpPatch("UpdateShip/{id}")]
        public async Task<IActionResult> UpdateShip(Guid id, [FromQuery] int state = 0)
        {
            return Ok(await _mediator.Send(new UpdateShipRequest() { State = state, Id = id }));
        }
    }
}
