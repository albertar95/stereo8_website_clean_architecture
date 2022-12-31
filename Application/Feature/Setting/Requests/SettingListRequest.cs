using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Setting.Requests
{
    public class SettingListRequest : IRequest<List<Domain.Setting>>
    {
        public int State { get; set; }
        public int Pagesize { get; set; } = 100!;
        public int Skip { get; set; } = 0!;
    }
}
