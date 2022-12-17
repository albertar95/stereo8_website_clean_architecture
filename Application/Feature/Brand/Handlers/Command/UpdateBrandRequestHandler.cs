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

        public Task<bool> Handle(UpdateBrandRequest request, CancellationToken cancellationToken)
        {
            var brand = _mapper.Map<Domain.Brand>(request.brand);
            return _categoryRepository.UpdateBrand(brand);
        }
    }
}
