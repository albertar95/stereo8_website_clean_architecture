using Application.DTO.User;
using Application.Email.Contract;
using Application.Email.Model;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CommunicationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailSettings _mailsettings;
        private readonly IMailActions _mailActions;

        public MailController(IOptions<MailSettings> mailsettings,IMailActions mailActions)
        {
            _mailsettings = mailsettings.Value;
            _mailActions = mailActions;
        }
        [HttpPost("SendUserVerificationMail")]
        public IActionResult SendUserVerificationMail([FromBody] UserDto user) 
        {
            return Ok(_mailActions.VerificationMail(user.Id,user.Username));
        }
        [HttpPost("SendForgetPasswordMail")]
        public IActionResult SendForgetPasswordMail([FromBody] UserDto user) 
        {
            return Ok(_mailActions.ForgetPasswordMail(user));
        }
    }
}
