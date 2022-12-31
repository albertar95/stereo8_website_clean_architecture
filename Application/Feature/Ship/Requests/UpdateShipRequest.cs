using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Ship.Requests
{
    public class UpdateShipRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
        public int State { get; set; }
    }
}
