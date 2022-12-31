using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.File.Requests
{
    public class DeleteFileRequest : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
