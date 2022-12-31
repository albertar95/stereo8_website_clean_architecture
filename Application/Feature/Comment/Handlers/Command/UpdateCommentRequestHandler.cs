using Application.Feature.Comment.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Handlers.Command
{
    public class UpdateCommentRequestHandler : IRequestHandler<UpdateCommentRequest, bool>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;

        public UpdateCommentRequestHandler(IGeneralRepository generalRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
        {
            var comment = await _generalRepository.GetComment(request.Id);
            comment.State = request.State;
            return await _generalRepository.Update(comment);
        }
    }
}
