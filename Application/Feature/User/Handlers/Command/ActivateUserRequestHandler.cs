using Application.Feature.User.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Handlers.Command
{
    public class ActivateUserRequestHandler : IRequestHandler<ActivateUserRequest,bool>
    {
        private readonly IUserRepository _userRepository;

        public ActivateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(ActivateUserRequest request, CancellationToken cancellationToken)
        {
            return await _userRepository.ActivateUser(request.Id);
        }
    }
}
