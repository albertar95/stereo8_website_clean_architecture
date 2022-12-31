using Application.Persistance.Contracts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IFileRepository : IBaseRepository
    {
        Task<IEnumerable<Domain.File>> GetFiles(Guid NidRelate);
        Task<Domain.File> GetFile(Guid NidFile);
        Task<IEnumerable<Domain.File>> GetCommonFiles(int RelateTypeStart = 16, int RelateTypeEnd = 29);
    }
}
