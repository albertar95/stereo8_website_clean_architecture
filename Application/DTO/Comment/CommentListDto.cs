using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Comment
{
    public class CommentListDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string? PersianCreateDate { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid ProductId { get; set; }
        public string? CommentText { get; set; }
        public byte Review { get; set; }
    }
}
