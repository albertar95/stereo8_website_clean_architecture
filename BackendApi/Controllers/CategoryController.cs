using Application.DTO.Brand;
using Application.DTO.Category;
using Application.DTO.Product;
using Application.DTO.Type;
using Application.Feature.Brand.Requests;
using Application.Feature.Category.Requests;
using Application.Feature.Product.Requests;
using Application.Feature.Type.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //category section
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto categoryDto) 
        {
            return Ok(await _mediator.Send(new CreateCategoryRequest() { category = categoryDto }));
        }
        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto categoryDto)
        {
            return Ok(await _mediator.Send(new UpdateCategoryRequest() { category = categoryDto }));
        }
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteCategoryRequest() { Id = id }));
        }
        [HttpGet("GetCategoryList")]
        public async Task<IActionResult> GetCategoryList([FromQuery]int state = 0) 
        {
            return Ok(await _mediator.Send(new GetCategoryListRequest(){ State = state}));
        }
        //brand section
        [HttpPost("CreateBrand")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandDto brandDto)
        {
            return Ok(await _mediator.Send(new CreateBrandRequest() { brand = brandDto }));
        }
        [HttpPatch("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandDto brandDto)
        {
            return Ok(await _mediator.Send(new UpdateBrandRequest() { brand = brandDto }));
        }
        [HttpDelete("DeleteBrand/{id}")]
        public async Task<IActionResult> DeleteBrand(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteBrandRequest() { Id = id }));
        }
        //type section
        [HttpPost("CreateType")]
        public async Task<IActionResult> CreateType([FromBody] CreateTypeDto typeDto)
        {
            return Ok(await _mediator.Send(new CreateTypeRequest() { type = typeDto }));
        }
        [HttpPatch("UpdateType")]
        public async Task<IActionResult> UpdateType([FromBody] UpdateTypeDto typeDto)
        {
            return Ok(await _mediator.Send(new UpdateTypeRequest() { type = typeDto }));
        }
        [HttpDelete("DeleteType/{id}")]
        public async Task<IActionResult> DeleteType(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteTypeRequest() { Id = id }));
        }
        //product section
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto productDto)
        {
            return Ok(await _mediator.Send(new CreateProductRequest() { product = productDto }));
        }
        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductDto productDto)
        {
            return Ok(await _mediator.Send(new UpdateProductRequest() { product = productDto }));
        }
        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            return Ok(await _mediator.Send(new DeleteProductRequest() { Id = id }));
        }
    }
}
