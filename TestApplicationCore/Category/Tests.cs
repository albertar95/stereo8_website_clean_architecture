using Application.Feature.Category.Handlers.Command;
using Application.Persistance.Contracts;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApplicationCore.Category
{
    public class Tests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public Tests(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        [Fact]
        public void UpdateCategoryTest()
        {
            var result = new UpdateCategoryRequestHandler(_categoryRepository, _mapper).Handle(
                new Application.Feature.Category.Requests.UpdateCategoryRequest() 
                {  category = new Application.DTO.Category.UpdateCategoryDto() 
                {  Id = Guid.Parse("E9C70BC7-5698-4866-84D4-F26055CE9D77"), CategoryName = "sth", Description = "sth", Keywords = "sth"} },new CancellationToken());
        }
    }
}
