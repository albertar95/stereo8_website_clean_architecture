using Application.Persistance.Contracts.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> LoginWithUsername(string username, string password);
        Task<bool> CheckUsernameExist(string username);
        Task<User> GetUserById(Guid NidUser, bool IncludeAll = true);
        Task<User> GetUserByUsername(string Username, bool IncludeAll = false);
        Task<bool> ActivateUser(Guid NidUser);
    }
}
