using Application.DTO.User;
using Application.Feature.User.Requests;
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

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
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
            return Ok(await _mediator.Send(new CreateUserRequest() { User = user }));
        }
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLogin login)
        {
            return Ok(await _mediator.Send(new LoginWithUsernameRequest() {  Username = login.Username, Password = login.Password }));
        }
    }
}
