using Application.DTO.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Requests
{
    public class CommentListRequest : IRequest<List<CommentListDto>>
    {
        public int State { get; set; }
        public int Pagesize { get; set; } = 100!;
        public int Skip { get; set; } = 0!;
    }
}
