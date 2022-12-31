using Application.DTO.Brand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Brand.Requests
{
    public class GetBrandRequest : IRequest<BrandDto>
    {
        public Guid Id { get; set; }
    }
}
