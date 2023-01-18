using Application.DTO.Category;
using Application.Helper;
using Application.Persistance.Contracts;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCore
{
    public static class CategoryRepository
    {
        public static Mock<ICategoryRepository> GetCategoryRepository()
        {
            var categories = new List<Domain.Category> {
                new Domain.Category{ Id = Guid.NewGuid(), CategoryName = "test1", CreateDate = DateTime.Now, PersianCreateDate = Commons.GetPersianDate(DateTime.Now), State = 0 },
                new Domain.Category{ Id = Guid.NewGuid(), CategoryName = "test2", CreateDate = DateTime.Now, PersianCreateDate = Commons.GetPersianDate(DateTime.Now), State = 0 },
            };
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(r => r.GetAllCategories()).ReturnsAsync(categories);
            mockRepo.Setup(r => r.GetCategoryByTitle("", false)).ReturnsAsync(categories.FirstOrDefault(p => p.CategoryName == "test1") ?? new Domain.Category());
            mockRepo.Setup(r => r.Create(It.IsAny<Domain.Category>())).ReturnsAsync((Domain.Category category) => { categories.Add(category); return true; });
            return mockRepo;
        }
        public static Mock<IMediator> getMediator() 
        {
            var reqNhandlers = new List<Tuple<getCategoryById, getCategoryByIdHandler>> {
                new Tuple<getCategoryById, getCategoryByIdHandler>(new getCategoryById(),new getCategoryByIdHandler())
            };
            var mediator = new Mock<IMediator>();
            mediator.Setup(r => r.Send(reqNhandlers.FirstOrDefault().Item1,CancellationToken.None)).Returns(reqNhandlers.FirstOrDefault().Item2.Handle(reqNhandlers.FirstOrDefault().Item1,CancellationToken.None));
            return mediator;
        }
    }
    public class getCategoryById : IRequest<CategoryListDto>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
    public class getCategoryByIdHandler : IRequestHandler<getCategoryById, CategoryListDto>
    {
        public async Task<CategoryListDto> Handle(getCategoryById request, CancellationToken cancellationToken)
        {
            return new CategoryListDto() { Id = request.Id, CategoryName = "test3", ProductCount = 0 };
        }

        public static explicit operator Task<object>(getCategoryByIdHandler v)
        {
            throw new NotImplementedException();
        }
    }
}
