using Application.DTO.Product;
using Application.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests.UIProjections
{
    public class GetFilteredProductsRequest : IRequest<List<ProductListDto>>
    {
        public UIProductFilters Filters { get; set; }
    }
}
