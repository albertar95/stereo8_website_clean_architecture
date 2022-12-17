using Application.DTO.Category;
using Application.Feature.Category.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/backend/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<bool> CreateCategory([FromBody] CreateCategoryDto categoryDto) 
        {
            return await _mediator.Send(new CreateCategoryRequest() { category = categoryDto});
        }
    }
}
