using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.File
{
    public class FileListDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; } = null!;
        public Guid RelateId { get; set; }
        public byte RelateType { get; set; }
        public string FileUrl { get; set; } = null!;
    }
}
