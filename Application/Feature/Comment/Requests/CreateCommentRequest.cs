using Application.DTO.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Requests
{
    public class CreateCommentRequest : IRequest<bool>
    {
        public CreateCommentDto Comment { get; set; }
    }
}
