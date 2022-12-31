using Application.DTO.Ship;
using Application.Feature.Ship.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Ship.Handlers.Query
{
    public class ShipListRequestHandler : IRequestHandler<ShipListRequest, List<ShipListDto>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public ShipListRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<List<ShipListDto>> Handle(ShipListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<ShipListDto>>(await _purchaseRepository.GetShips(request.State, request.Pagesize, request.Skip));
        }
    }
}
