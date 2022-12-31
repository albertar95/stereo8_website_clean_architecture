using Application.Feature.Cart.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Cart.Handlers.Command
{
    public class UpdateCartRequestHandler : IRequestHandler<UpdateCartRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public UpdateCartRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<bool> Handle(UpdateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = await _purchaseRepository.GetCartById(request.Cart.Id);
            cart.Quantity = request.Cart.Quantity;
            return await _purchaseRepository.Update(cart);
        }
    }
}
