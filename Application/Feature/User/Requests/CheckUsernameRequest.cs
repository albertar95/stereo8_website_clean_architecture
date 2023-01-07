using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Requests
{
    public class CheckUsernameRequest : IRequest<bool>
    {
        public string Username { get; set; }
    }
}
