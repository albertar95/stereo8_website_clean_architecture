﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Brand
{
    public class BrandListDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string BrandName { get; set; } = null!;
        public Guid CategoryId { get; set; }
    }
}
