using Application.DTO.File;
using Application.Feature.File.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.File.Handlers.Query
{
    public class getFileRequestHandler : IRequestHandler<GetFileRequest,FileDto>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public getFileRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileDto> Handle(GetFileRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<FileDto>(await _fileRepository.GetFile(request.Id));
        }
    }
}
