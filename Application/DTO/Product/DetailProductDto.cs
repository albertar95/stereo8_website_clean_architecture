﻿using Application.DTO.Comment;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Product
{
    public class DetailProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string BrandName { get; set; } = null!;
        public string TypeName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public Guid TypeId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
        public decimal Price { get; set; }
        public byte Priority { get; set; }
        public byte OffPercentage { get; set; }
        public decimal PriceWithoutOff { get; set; }
        public int AvailableCount { get; set; }
        public string? Specification { get; set; }
        public string? DetailDesc { get; set; }
        public string UserName { get; set; } = null!;
        public byte? Rating { get; set; }
        public double? Weight { get; set; }
        public string? PersianCreateDate { get; set; }
        public string? PersianLastModified { get; set; }
        public ICollection<CommentListDto> Comments { get; } = new List<CommentListDto>();
    }
}
