using Application.DTO.Order;
using Application.Feature.Order.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Order.Handlers.Query
{
    public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public GetOrderRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<OrderDto> Handle(GetOrderRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<OrderDto>(await _purchaseRepository.GetOrder(request.Id));
        }
    }
}
