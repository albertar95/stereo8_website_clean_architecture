using Application.DTO.User;
using Application.Feature.User.Requests;
using Application.Helper;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Handlers.Command
{
    public class LoginWithUsernameRequestHandler :IRequestHandler<LoginWithUsernameRequest,UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginWithUsernameRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(LoginWithUsernameRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userRepository.LoginWithUsername(request.Username,Commons.EncryptString(request.Password.Trim())));
        }
    }
}
