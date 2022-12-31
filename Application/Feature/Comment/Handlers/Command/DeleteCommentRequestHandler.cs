using Application.Feature.Comment.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Handlers.Command
{
    public class DeleteCommentRequestHandler : IRequestHandler<DeleteCommentRequest, bool>
    {
        private readonly IGeneralRepository _generalRepository;

        public DeleteCommentRequestHandler(IGeneralRepository generalRepository)
        {
            _generalRepository = generalRepository;
        }
        public async Task<bool> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
        {
            return await _generalRepository.DeleteComment(request.Id);
        }
    }
}
