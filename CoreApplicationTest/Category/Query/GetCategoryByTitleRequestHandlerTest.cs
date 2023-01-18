using Application.DTO.Category;
using Application.Feature.Category.Handlers.Query;
using Application.Feature.Category.Requests;
using Application.Mapper;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCore;

namespace CoreApplicationTest.Category.Query
{
    public class GetCategoryByTitleRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _repo;
        public GetCategoryByTitleRequestHandlerTest()
        {
            _repo = CategoryRepository.GetCategoryRepository();
            _mapper = new MapperConfiguration(c => { c.AddProfile<MapperProfile>(); }).CreateMapper();
        }
        [Fact]
        public async Task GetCategoryByTitle() 
        {
            var handler = new GetCategoryByTitleRequestHandler(_repo.Object,_mapper);
            var result = await handler.Handle(new GetCategoryByTitleRequest(), CancellationToken.None);
            result.ShouldBeOfType<CategoryDto>();
        }
    }
}
