using Application.Feature.User.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Handlers.Query
{
    public class CheckUsernameRequestHandler : IRequestHandler<CheckUsernameRequest,bool>
    {
        private readonly IUserRepository _userRepository;

        public CheckUsernameRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(CheckUsernameRequest request, CancellationToken cancellationToken)
        {
            return await _userRepository.CheckUsernameExist(request.Username);
        }
    }
}
