using Application.DTO.Category;
using Application.Feature.Category.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Category.Handlers.Query
{
    public class GetCategoryByTitleRequestHandler : IRequestHandler<GetCategoryByTitleRequest,CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByTitleRequestHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByTitleRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CategoryDto>(await _categoryRepository.GetCategoryByTitle(request.Title, request.IncludeProduct));
        }
    }
}
