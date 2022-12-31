using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Comment
{
    public class CreateCommentDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? CommentText { get; set; }
        public byte Review { get; set; }
    }
}
