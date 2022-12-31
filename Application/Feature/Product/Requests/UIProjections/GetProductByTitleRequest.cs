using Application.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests.UIProjections
{
    public class GetProductByTitleRequest : IRequest<DetailProductDto>
    {
        public string Title { get; set; } = ""!;
        public bool IncludeAll { get; set; } = true!;
    }
}
