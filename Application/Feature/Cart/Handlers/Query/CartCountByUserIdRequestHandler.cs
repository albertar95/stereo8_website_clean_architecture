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
    public class CartCountByUserIdRequestHandler : IRequestHandler<CartCountByUserIdRequest,int>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public CartCountByUserIdRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<int> Handle(CartCountByUserIdRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.GetCartCountByUserId(request.UserId);
        }
    }
}
