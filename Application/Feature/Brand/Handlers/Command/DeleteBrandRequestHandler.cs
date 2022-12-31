using Application.Feature.Brand.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Brand.Handlers.Command
{
    public class DeleteBrandRequestHandler : IRequestHandler<DeleteBrandRequest, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteBrandRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteBrandRequest request, CancellationToken cancellationToken)
        {
            var tmpBrand = await _categoryRepository.GetBrand(request.Id);
            if (tmpBrand == null)
                return false;
            else
            {
                tmpBrand.State = 1;
                var brand = _mapper.Map<Domain.Brand>(tmpBrand);
                return await _categoryRepository.Update(brand);
            }
        }
    }
}
