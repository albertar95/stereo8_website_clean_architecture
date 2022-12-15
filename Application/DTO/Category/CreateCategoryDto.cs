using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Category
{
    public class CreateCategoryDto
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public string? Keywords { get; set; }
    }
}
