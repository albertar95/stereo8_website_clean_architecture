using Application.DTO.Brand;
using Application.Feature.Brand.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Brand.Handlers.Query
{
    public class GetBrandRequestHandler : IRequestHandler<GetBrandRequest,BrandDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetBrandRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<BrandDto> Handle(GetBrandRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<BrandDto>(await _categoryRepository.GetBrand(request.Id));
        }
    }
}
