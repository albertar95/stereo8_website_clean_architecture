﻿using Application.DTO.Brand;
using Application.DTO.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Category
{
    public class CategoryListDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public int ProductCount { get; set; }
        public ICollection<BrandListDto> Brands { get; } = new List<BrandListDto>();
        public ICollection<TypeListDto> Types { get; } = new List<TypeListDto>();
    }
}
