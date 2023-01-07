using Application.Feature.User.Requests;
using Application.Helper;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Handlers.Command
{
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest,Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.User>(request.User);
            user.Password = Commons.EncryptString(user.Password);
            if (await _userRepository.Create(user))
                return user.Id;
            else
                return Guid.Empty;
        }
    }
}
