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
    public class DeleteCartRequestHandler : IRequestHandler<DeleteCartRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public DeleteCartRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<bool> Handle(DeleteCartRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.DeleteCart(request.Id);
        }
    }
}
