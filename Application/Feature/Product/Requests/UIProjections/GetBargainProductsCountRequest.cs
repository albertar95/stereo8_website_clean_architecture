using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Requests.UIProjections
{
    public class GetBargainProductsCountRequest : IRequest<int>
    {
    }
}
