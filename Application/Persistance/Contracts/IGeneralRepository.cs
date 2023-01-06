using Application.Persistance.Contracts.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IGeneralRepository : IBaseRepository
    {
        Task<IEnumerable<Domain.Setting>> GetSettings(int Pagesize = 100, int Skip = 0, int State = 0);
        Task<IEnumerable<Comment>> GetComments(int State = 0, int Pagesize = 100, int Skip = 0);
        Task<Domain.Setting> GetSetting(Guid NidSetting, byte State = 0);
        Task<Comment> GetComment(Guid NidComment);
        Task<string[]> GetIndexPageValues();
        Task<bool> DeleteComment(Guid Id);
        Task<bool> DeleteSetting(Guid Id);
    }
}
