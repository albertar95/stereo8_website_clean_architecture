using Application.Feature.Comment.Requests;
using Application.Feature.Misc.Requests;
using Application.Feature.Order.Requests;
using Application.Feature.Setting.Requests;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GeneralController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments([FromQuery] int state = 0, [FromQuery] int pageSize = 100, int skip = 0)
        {
            return Ok(await _mediator.Send(new CommentListRequest() { State = state, Pagesize = pageSize, Skip = skip }));
        }
        [HttpDelete("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCommentRequest() { Id = id }));
        }
        [HttpPatch("UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromQuery] int state = 0)
        {
            return Ok(await _mediator.Send(new UpdateCommentRequest() { Id = id, State = state }));
        }
        [HttpGet("GetSettings")]
        public async Task<IActionResult> GetSettings([FromQuery] int state = 0, [FromQuery] int pageSize = 100, int skip = 0)
        {
            try
            {
                return Ok(await _mediator.Send(new SettingListRequest() { State = state, Pagesize = pageSize, Skip = skip }));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CreateSetting")]
        public async Task<IActionResult> CreateSetting([FromBody] Setting setting)
        {
            return Ok(await _mediator.Send(new CreateSettingRequest() { Setting = setting }));
        }
        [HttpDelete("DeleteSetting/{id}")]
        public async Task<IActionResult> DeleteSetting(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteSettingRequest() { Id = id }));
        }
        [HttpPatch("UpdateSetting")]
        public async Task<IActionResult> UpdateSetting([FromBody] Setting setting)
        {
            return Ok(await _mediator.Send(new UpdateSettingRequest() { Setting = setting }));
        }
        [HttpGet("GetIndexPageValues")]
        public async Task<IActionResult> GetIndexPageValues()
        {
            return Ok(await _mediator.Send(new GetIndexPageInformationRequest()));
        }
    }
}
