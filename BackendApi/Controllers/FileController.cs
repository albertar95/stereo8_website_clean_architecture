using Application.DTO.File;
using Application.Feature.File.Requests;
using Infra.ImageManager;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetFile/{id}")]
        public async Task<IActionResult> GetFile(Guid id) 
        {
            return Ok(await _mediator.Send(new GetFileRequest() {  Id = id }));
        }
        [HttpGet("GetFileList/{relateId}")]
        public async Task<IActionResult> GetFileList(Guid relateId)
        {
            return Ok(await _mediator.Send(new GetFileListRequest() { RelateId = relateId }));
        }
        [HttpGet("GetCommonFiles")]
        public async Task<IActionResult> GetCommonFiles()
        {
            return Ok(await _mediator.Send(new GetCommonFilesRequest()));
        }
        [HttpPost("CreateFile")]
        public async Task<IActionResult> CreateFile([FromBody] CreateFileDto file)
        {
            if (ImageReduce.Save(file.Width, file.Height, file.FilePath, file.TheFile))
            {
                return Ok(await _mediator.Send(new CreateFileRequest() { file = file }));
            }
            else
                return BadRequest();
        }
        [HttpDelete("DeleteFile/{id}")]
        public async Task<IActionResult> DeleteFile(Guid id)
        {
            string tmpFilePath = await _mediator.Send(new DeleteFileRequest() { Id = id });
            if (tmpFilePath != string.Empty)
                return Ok(ImageReduce.Remove(tmpFilePath));
            else
                return BadRequest();
        }
    }
}
