using Application.DTO.Favorite;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Favorite.Requests
{
    public class CreateFavoriteRequest : IRequest<bool>
    {
        public CreateFavoriteDto Favorite { get; set; }
    }
}
