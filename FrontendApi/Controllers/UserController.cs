using Application.DTO.User;
using Application.Feature.User.Requests;
using Application.MessageBroker.Bus;
using Application.MessageBroker.Events;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IEventBus _eventBus;

        public UserController(IMediator mediator, IEventBus eventBus)
        {
            _mediator = mediator;
            _eventBus = eventBus;
        }
        [HttpGet("GetUserByUsername/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username) 
        {
            return Ok(await _mediator.Send(new GetUserByUsernameRequest() {  Username = username }));
        }
        [HttpGet("CheckUsernameExist/{username}")]
        public async Task<IActionResult> CheckUsernameExist(string username)
        {
            return Ok(await _mediator.Send(new CheckUsernameRequest() { Username = username }));
        }
        [HttpPatch("ActivateUser/{id}")]
        public async Task<IActionResult> ActivateUser(Guid id)
        {
            return Ok(await _mediator.Send(new ActivateUserRequest() {  Id = id }));
        }
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserDto user)
        {
            var id = await _mediator.Send(new CreateUserRequest() { User = user });
            if (id != Guid.Empty)
            {
                _eventBus.Publish(new UserCreatedEvent(id,user.Username));
                return Ok(id);
            }
            else
                return BadRequest();
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin login)
        {
            return Ok(await _mediator.Send(new LoginWithUsernameRequest() {  Username = login.Username, Password = login.Password }));
        }
    }
}
