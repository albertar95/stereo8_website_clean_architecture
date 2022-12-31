using Application.DTO.Favorite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Requests
{
    public class FavoriteListRequest : IRequest<List<FavoriteListDto>>
    {
        public Guid UserId { get; set; }
    }
}
