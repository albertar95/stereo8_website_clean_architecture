using Application.Feature.ShipPrice.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.ShipPrice.Handlers.Query
{
    public class ShipPriceListRequestHandler : IRequestHandler<ShipPriceListRequest, IEnumerable<Domain.ShipPrice>>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public ShipPriceListRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<IEnumerable<Domain.ShipPrice>> Handle(ShipPriceListRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.GetShipPrices();
        }
    }
}
