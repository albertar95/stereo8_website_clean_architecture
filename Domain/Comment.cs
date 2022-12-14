using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Comment
    {
        public Guid NidComment { get; set; }

        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public string? CommentText { get; set; }

        public byte Review { get; set; }

        public byte State { get; set; }

        public DateTime CreateDate { get; set; }

        public string? PersianCreateDate { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
