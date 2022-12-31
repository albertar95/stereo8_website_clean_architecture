using Application.Feature.Ship.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Ship.Handlers.Command
{
    public class UpdateShipRequestHandler : IRequestHandler<UpdateShipRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public UpdateShipRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(UpdateShipRequest request, CancellationToken cancellationToken)
        {
            var ship = await _purchaseRepository.GetShip(request.Id);
            ship.State = request.State;
            return await _purchaseRepository.Update(ship);
        }
    }
}
