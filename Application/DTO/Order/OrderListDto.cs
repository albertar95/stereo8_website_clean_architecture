using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Order
{
    public class OrderListDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string? PersianCreateDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? CityId { get; set; }
        public string? Tel { get; set; }
    }
}
