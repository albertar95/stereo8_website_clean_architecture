using Application.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests
{
    public class GetProductListRequest : IRequest<List<ProductListDto>>
    {
        public int State { get; set; }
        public int PageSize { get; set; } = 100!;
        public int Skip { get; set; } = 0!;
    }
}
