using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public string? PersianCreateDate { get; set; }
        public string? PersianLastModified { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? PersianLastLoginDate { get; set; }
        public decimal? ZipCode { get; set; }
        public string? Address { get; set; }
        public string? Tel { get; set; }
    }
}
