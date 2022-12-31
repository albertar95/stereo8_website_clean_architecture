using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Comment.Requests
{
    public class UpdateCommentRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int State { get; set; }
    }
}
