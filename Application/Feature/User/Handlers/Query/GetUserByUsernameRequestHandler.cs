using Application.DTO.User;
using Application.Feature.User.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.User.Handlers.Query
{
    public class GetUserByUsernameRequestHandler : IRequestHandler<GetUserByUsernameRequest,UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByUsernameRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByUsernameRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<UserDto>(await _userRepository.GetUserByUsername(request.Username));
        }
    }
}
