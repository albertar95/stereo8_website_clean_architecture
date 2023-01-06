using Application.Feature.Cart.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Cart.Handlers.Query
{
    public class CartAggregateByUserIdRequestHandler : IRequestHandler<CartAggregateByUserIdRequest,decimal>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public CartAggregateByUserIdRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<decimal> Handle(CartAggregateByUserIdRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.GetCartAggregateByUserId(request.UserId);
        }
    }
}
