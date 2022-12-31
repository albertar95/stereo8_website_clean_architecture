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
    public class OrderListRequestHandler : IRequestHandler<OrderListRequest, List<OrderListDto>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public OrderListRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderListDto>> Handle(OrderListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<OrderListDto>>(await _purchaseRepository.GetOrders(request.State,false,request.Pagesize,request.Skip));
        }
    }
}
