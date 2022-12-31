using Application.Feature.Favorite.Requests;
using Application.Persistance.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Handlers.Command
{
    public class DeleteFavoriteRequestHandler : IRequestHandler<DeleteFavoriteRequest, bool>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public DeleteFavoriteRequestHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<bool> Handle(DeleteFavoriteRequest request, CancellationToken cancellationToken)
        {
            return await _purchaseRepository.DeleteFavorite(request.Id);
        }
    }
}
