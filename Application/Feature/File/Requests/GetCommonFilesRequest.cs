using Application.DTO.File;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.File.Requests
{
    public class GetCommonFilesRequest : IRequest<List<FileListDto>>
    {
        public int Start { get; set; } = 16!;
        public int End { get; set; } = 29!;
    }
}
