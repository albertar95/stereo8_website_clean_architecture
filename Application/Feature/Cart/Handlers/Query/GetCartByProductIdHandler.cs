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
    public class GetCartByProductIdHandler : IRequestHandler<GetCartByProductId,CartDto>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public GetCartByProductIdHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> Handle(GetCartByProductId request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CartDto>(await _purchaseRepository.GetCartByProductId(request.UserId,request.ProductId));
        }
    }
}
