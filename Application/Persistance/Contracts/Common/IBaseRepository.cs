using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts.Common
{
    public interface IBaseRepository
    {
        Task<bool> Create<T>(T item);
        Task<bool> Update<T>(T item);
        Task<bool> Delete<T>(T item);
    }
}
