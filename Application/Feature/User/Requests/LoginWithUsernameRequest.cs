using Application.DTO.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Requests
{
    public class LoginWithUsernameRequest : IRequest<UserDto>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
