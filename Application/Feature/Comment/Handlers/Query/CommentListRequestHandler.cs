using Application.DTO.Comment;
using Application.Feature.Comment.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Handlers.Query
{
    public class CommentListRequestHandler : IRequestHandler<CommentListRequest, List<CommentListDto>>
    {
        private readonly IGeneralRepository _generalRepository;
        private readonly IMapper _mapper;

        public CommentListRequestHandler(IGeneralRepository generalRepository, IMapper mapper)
        {
            _generalRepository = generalRepository;
            _mapper = mapper;
        }
        public async Task<List<CommentListDto>> Handle(CommentListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CommentListDto>>(await _generalRepository.GetComments(request.State,request.Pagesize,request.Skip));
        }
    }
}
