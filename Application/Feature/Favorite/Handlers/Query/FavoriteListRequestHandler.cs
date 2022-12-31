using Application.DTO.Favorite;
using Application.Feature.Favorite.Requests;
using Application.Persistance.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Handlers.Query
{
    public class FavoriteListRequestHandler : IRequestHandler<FavoriteListRequest, List<FavoriteListDto>>
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public FavoriteListRequestHandler(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }
        public async Task<List<FavoriteListDto>> Handle(FavoriteListRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<List<FavoriteListDto>>(await _purchaseRepository.GetFavoritesByUserId(request.UserId));
        }
    }
}
