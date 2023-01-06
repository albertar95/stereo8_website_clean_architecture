using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
