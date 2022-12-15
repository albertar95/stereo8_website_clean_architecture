using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Type
{
    public class TypeDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; } = null!;
        public Guid CategoryId { get; set; }
    }
}
