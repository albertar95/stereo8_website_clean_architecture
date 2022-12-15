﻿using Domain.Common;
using System;
using System.Collections.Generic;

namespace Domain 
{
    public class Comment : BaseDomainProperties
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public string? CommentText { get; set; }
        public byte Review { get; set; }
        public virtual Product Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
