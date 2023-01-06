using Application.Feature.Comment.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Handlers.Command
{
    public class CreateCommentRequestHandler : IRequestHandler<CreateCommentRequest, bool>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;

        public CreateCommentRequestHandler(IGeneralRepository generalRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = _mapper.Map<Domain.Comment>(request.Comment);
            return await _generalRepository.Create(comment);
        }
    }
}
