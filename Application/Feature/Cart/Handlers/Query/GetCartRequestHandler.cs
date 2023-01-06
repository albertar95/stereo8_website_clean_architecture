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
    public class GetCartRequestHandler : IRequestHandler<GetCartRequest,CartDto>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public GetCartRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> Handle(GetCartRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CartDto>(await _purchaseRepository.GetCartById(request.Id));
        }
    }
}
