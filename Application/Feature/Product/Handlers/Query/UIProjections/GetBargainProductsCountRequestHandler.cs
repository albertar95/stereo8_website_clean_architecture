using Application.Feature.Product.Requests.UIProjections;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Product.Handlers.Query.UIProjections
{
    public class GetBargainProductsCountRequestHandler : IRequestHandler<GetBargainProductsCountRequest,int>
    {
        private readonly IProductRepository _productRepository;

        public GetBargainProductsCountRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(GetBargainProductsCountRequest request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetBargainProductCount();
        }
    }
}
