using Application.Feature.Brand.Requests;
using Application.Feature.Category.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Handlers.Command
{
    public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var tmpCategory = await _categoryRepository.GetCategory(request.Id);
            if (tmpCategory == null)
                return false;
            else
            {
                tmpCategory.State = 1;
                var category = _mapper.Map<Domain.Category>(tmpCategory);
                return await _categoryRepository.UpdateCategory(category);
            }
        }
    }
}
