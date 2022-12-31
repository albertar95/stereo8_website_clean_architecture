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
    public class GetFileListRequestHandler : IRequestHandler<GetFileListRequest,List<FileListDto>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public GetFileListRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<List<FileListDto>> Handle(GetFileListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FileListDto>>(await _fileRepository.GetFiles(request.RelateId));
        }
    }
}
