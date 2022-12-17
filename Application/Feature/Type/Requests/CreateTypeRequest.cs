using Application.DTO.Type;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Type.Requests
{
    public class CreateTypeRequest : IRequest<bool>
    {
        public CreateTypeDto type { get; set; } = null!;
    }
}
