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
    public class CreateFileRequestHandler : IRequestHandler<CreateFileRequest,bool>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public CreateFileRequestHandler(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateFileRequest request, CancellationToken cancellationToken)
        {
            var file = _mapper.Map<Domain.File>(request.file);
            return await _fileRepository.Create(file);
        }
    }
}
