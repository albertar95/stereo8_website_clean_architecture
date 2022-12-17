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
    public class CreateCategoryRequestHandler : IRequestHandler<CreateCategoryRequest, bool>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public Task<bool> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Domain.Category>(request.category);
            return _categoryRepository.CreateCategory(category);
        }
    }
}
