using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Ship
{
    public class ShipListDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string? PersianCreateDate { get; set; }
        public byte ShipVia { get; set; }
        public decimal ShipPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
    }
}
