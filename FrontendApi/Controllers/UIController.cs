using Application.DTO.Comment;
using Application.Feature.Category.Requests;
using Application.Feature.Comment.Requests;
using Application.Feature.File.Requests;
using Application.Feature.Product.Requests;
using Application.Feature.Product.Requests.UIProjections;
using Application.Feature.Setting.Requests;
using Application.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UIController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UIController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetFiles/{relateId}")]
        public async Task<IActionResult> GetFiles(Guid relateId) 
        {
            return Ok(await _mediator.Send(new GetFileListRequest() {  RelateId = relateId}));
        }
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories([FromQuery] bool includeProduct = false, [FromQuery] int state = 0)
        {
            return Ok(await _mediator.Send(new GetCategoryListRequest() {  State = state, IncludeProduct = includeProduct }));
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts([FromQuery] int pageSize = 10, [FromQuery] int skip = 0, [FromQuery] int state = 0)
        {
            return Ok(await _mediator.Send(new GetProductListRequest() { State = state, Skip = skip, PageSize = pageSize }));
        }
        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(Guid id, [FromQuery] bool includeProduct = false)
        {
            return Ok(await _mediator.Send(new GetCategoryRequest() { Id = id, IncludeProduct = includeProduct }));
        }
        [HttpGet("GetSpecialProducts")]
        public async Task<IActionResult> GetSpecialProducts([FromQuery] int pageSize = 10, [FromQuery] int skip = 0)
        {
            return Ok(await _mediator.Send(new GetSpecialProductsRequest() { PageSize = pageSize, Skip = skip }));
        }
        [HttpGet("GetOffProducts")]
        public async Task<IActionResult> GetOffProducts([FromQuery] int pageSize = 10, [FromQuery] int skip = 0)
        {
            return Ok(await _mediator.Send(new GetOffProductsRequest() {  PageSize = pageSize, Skip = skip }));
        }
        [HttpGet("GetCategoryByTitle/{title}")]
        public async Task<IActionResult> GetCategoryByTitle(string title, [FromQuery] bool includeProduct = false)
        {
            return Ok(await _mediator.Send(new GetCategoryByTitleRequest() { Title = title, IncludeProduct =  includeProduct }));
        }
        [HttpGet("GetProductsByCategoryId/{id}")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid id, [FromQuery] int pageSize = 10, [FromQuery] int skip = 0)
        {
            return Ok(await _mediator.Send(new GetProductsByCategoryIdRequest() { NidCategory = id, PageSize = pageSize, Skip = skip }));
        }
        [HttpPost("GetFilteredProducts")]
        public async Task<IActionResult> GetFilteredProducts([FromBody] UIProductFilters filters)
        {
            return Ok(await _mediator.Send(new Application.Feature.Product.Requests.UIProjections.GetFilteredProductsRequest() { Filters = filters }));
        }
        [HttpGet("GetProductByTitle/{title}")]
        public async Task<IActionResult> GetProductByTitle(string title, [FromQuery] bool includeAll = true)
        {
            return Ok(await _mediator.Send(new GetProductByTitleRequest() { Title = title, IncludeAll = includeAll }));
        }
        [HttpGet("GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            return Ok(await _mediator.Send(new GetProductRequest() { Id = id }));
        }
        [HttpGet("GetSimilarProducts/{id}")]
        public async Task<IActionResult> GetSimilarProducts(Guid id)
        {
            return Ok(await _mediator.Send(new GetSimilarProductsRequest() { NidProduct =  id }));
        }
        [HttpGet("SearchProduct/{text}")]
        public async Task<IActionResult> SearchProduct(string text)
        {
            return Ok(await _mediator.Send(new SearchProductRequest() { Input = text }));
        }
        [HttpGet("GetBargainedProducts")]
        public async Task<IActionResult> GetBargainedProducts([FromQuery] int pageSize = 10, [FromQuery] int skip = 0)
        {
            return Ok(await _mediator.Send(new GetBargainedProductsRequest() { PageSize = pageSize, Skip = skip}));
        }
        [HttpGet("GetCommonFiles")]
        public async Task<IActionResult> GetCommonFiles([FromQuery] int from = 16, [FromQuery] int to = 29)
        {
            return Ok(await _mediator.Send(new GetCommonFilesRequest() { Start = from, End = to }));
        }
        [HttpGet("GetProductCount/{categoryId}")]
        public async Task<IActionResult> GetProductCount(Guid categoryId)
        {
            return Ok(await _mediator.Send(new GetProductCountRequest() { NidCategory = categoryId }));
        }
        [HttpGet("GetBargainProductCount")]
        public async Task<IActionResult> GetBargainProductCount()
        {
            return Ok(await _mediator.Send(new GetBargainProductsCountRequest()));
        }
        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment([FromBody]CreateCommentDto comment)
        {
            return Ok(await _mediator.Send(new CreateCommentRequest() {  Comment = comment }));
        }
        [HttpPost("CreateSetting")]
        public async Task<IActionResult> CreateSetting([FromBody] Domain.Setting setting)
        {
            return Ok(await _mediator.Send(new CreateSettingRequest() { Setting = setting }));
        }
    }
}
