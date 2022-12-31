using Application.DTO.Cart;
using Application.Feature.Cart.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Cart.Handlers.Query
{
    public class CartListRequestHandler : IRequestHandler<CartListRequest, List<CartListDto>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public CartListRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<List<CartListDto>> Handle(CartListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<CartListDto>>(await _purchaseRepository.GetCartsByUserId(request.UserId));
        }
    }
}
