using Application.Feature.Brand.Requests;
using Application.Feature.Category.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Handlers.Command
{
    public class UpdateCategoryRequestHandler : IRequestHandler<UpdateCategoryRequest, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategory(request.category.Id);
            if (category == null) return false;
            else
            {
                _mapper.Map(request.category, category);
                return await _categoryRepository.Update(category);
            }
        }
    }
}
