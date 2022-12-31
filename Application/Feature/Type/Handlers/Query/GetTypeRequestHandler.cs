using Application.DTO.Type;
using Application.Feature.Type.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Type.Handlers.Query
{
    public class GetTypeRequestHandler : IRequestHandler<GetTypeRequest,TypeDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetTypeRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<TypeDto> Handle(GetTypeRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<TypeDto>(await _categoryRepository.GetType(request.Id));
        }
    }
}
