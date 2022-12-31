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
    public class GetProductCountRequestHandler : IRequestHandler<GetProductCountRequest, int>
    {
        private readonly IProductRepository _productRepository;

        public GetProductCountRequestHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> Handle(GetProductCountRequest request, CancellationToken cancellationToken)
        {
            return await _productRepository.GetProductCount(request.NidCategory);
        }
    }
}
