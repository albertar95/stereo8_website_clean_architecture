using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Type
{
    public class CreateTypeDto
    {
        public string TypeName { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Keywords { get; set; }
    }
}
