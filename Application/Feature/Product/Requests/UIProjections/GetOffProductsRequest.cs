using Application.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests.UIProjections
{
    public class GetOffProductsRequest : IRequest<List<ProductListDto>>
    {
        public int PageSize { get; set; } = 10!;
        public int Skip { get; set; } = 0!;
    }
}
