using Application.DTO.Ship;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Ship.Requests
{
    public class ShipListRequest : IRequest<List<ShipListDto>>
    {
        public int State { get; set; }
        public int Pagesize { get; set; } = 100!;
        public int Skip { get; set; } = 0!;
    }
}
