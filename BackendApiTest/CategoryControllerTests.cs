using BackendApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCore;

namespace BackendApiTest
{
    public class CategoryControllerTests
    {
        private readonly Mock<IMediator> _mock;
        public CategoryControllerTests()
        {
            _mock = CategoryRepository.getMediator();
        }
        [Fact]
        public async void GetCategoryByIdTest() 
        {
            var controller = new CategoryController(_mock.Object);
            var result = await controller.GetCategory(Guid.NewGuid());
            result.ShouldBeOfType<OkObjectResult>();
        }
    }
}
