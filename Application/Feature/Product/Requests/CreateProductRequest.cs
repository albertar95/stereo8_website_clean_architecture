using Application.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests
{
    public class CreateProductRequest : IRequest<bool>
    {
        public CreateProductDto product { get; set; } = null!;
    }
}
