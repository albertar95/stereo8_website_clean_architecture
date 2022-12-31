using Application.Feature.Favorite.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Handlers.Command
{
    public class CreateFavoriteRequestHandler : IRequestHandler<CreateFavoriteRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public CreateFavoriteRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(CreateFavoriteRequest request, CancellationToken cancellationToken)
        {
            var fav = _mapper.Map<Domain.Favorite>(request.Favorite);
            return await _purchaseRepository.Create(fav);
        }
    }
}
