using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Favorite
{
    public class CreateFavoriteDto
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
