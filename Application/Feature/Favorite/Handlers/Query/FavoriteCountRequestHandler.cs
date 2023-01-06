using Application.Feature.Favorite.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Handlers.Query
{
    public class FavoriteCountRequestHandler : IRequestHandler<FavoriteCountRequest,int>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public FavoriteCountRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<int> Handle(FavoriteCountRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.GetFavoriteCountByUserId(request.UserId);
        }
    }
}
