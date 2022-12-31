using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Order
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string? PersianCreateDate { get; set; }
        public string? PersianLastModified { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Description { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string? Address { get; set; }
        public decimal? ZipCode { get; set; }
        public string? Tel { get; set; }
        public string? Email { get; set; }
        public decimal? MelliCode { get; set; }
        public long? RefId { get; set; }
        public string? Username { get; set; }
        public List<string> ProductNames { get; set; } = new List<string>()!;
    }
}
