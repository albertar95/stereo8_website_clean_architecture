using Application.Feature.File.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.File.Handlers.Command
{
    public class DeleteFileRequestHandler : IRequestHandler<DeleteFileRequest,string>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public DeleteFileRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<string> Handle(DeleteFileRequest request, CancellationToken cancellationToken)
        {
            var tmpFile = await _fileRepository.GetFile(request.Id);
            if (await _fileRepository.Delete(tmpFile))
                return tmpFile.FilePath;
            else
                return string.Empty;
        }
    }
}
