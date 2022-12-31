using Application.DTO.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.File.Requests
{
    public class GetFileListRequest : IRequest<List<FileListDto>>
    {
        public Guid RelateId { get; set; }
    }
}
