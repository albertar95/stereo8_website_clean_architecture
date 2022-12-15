using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseDomainProperties
    {
        public Guid Id { get; set; }
        public int State { get; set; }
        public DateTime CreateDate { get; set; }
        public string? PersianCreateDate { get; set; }
        public DateTime? LastModified { get; set; }
        public string? PersianLastModified { get; set; }
    }
}
