using Application.Feature.Cart.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Cart.Handlers.Command
{
    public class CreateCartRequestHandler : IRequestHandler<CreateCartRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public CreateCartRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateCartRequest request, CancellationToken cancellationToken)
        {
            var cart = _mapper.Map<Domain.Cart>(request.Cart);
            return await _purchaseRepository.Create(cart);
        }
    }
}
