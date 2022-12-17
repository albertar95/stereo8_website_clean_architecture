using Application.DTO.Type;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Type.Requests
{
    public class UpdateTypeRequest : IRequest<bool>
    {
        public UpdateTypeDto type { get; set; } = null!;
    }
}
