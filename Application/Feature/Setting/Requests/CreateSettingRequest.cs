using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Setting.Requests
{
    public class CreateSettingRequest : IRequest<bool>
    {
        public Domain.Setting Setting { get; set; }
    }
}
