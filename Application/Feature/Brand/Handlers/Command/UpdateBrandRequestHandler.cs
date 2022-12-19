using Application.Feature.Brand.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Brand.Handlers.Command
{
    public class UpdateBrandRequestHandler : IRequestHandler<UpdateBrandRequest, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateBrandRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = await _categoryRepository.GetBrand(request.brand.Id);
            if (brand == null) return false;
            else
            {
                _mapper.Map(request.brand, brand);
                return await _categoryRepository.UpdateBrand(brand);
            }
        }
    }
}
