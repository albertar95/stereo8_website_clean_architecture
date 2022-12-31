using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Favorite
{
    public class FavoriteListDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int AvailableCount { get; set; }
    }
}
