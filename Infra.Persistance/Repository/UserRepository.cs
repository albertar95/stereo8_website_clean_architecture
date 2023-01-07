using Application.Persistance.Contracts;
using Domain;
using Infra.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly AudioShopDbContext _context;

        public UserRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ActivateUser(Guid NidUser)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == NidUser);
                if (user == null)
                    return false;
                else
                {
                    user.State = 0;
                    _context.Update(user);
                    if (await _context.SaveChangesAsync() == 1)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CheckUsernameExist(string username)
        {
            try
            {
                return await _context.Users.AnyAsync(p => p.Username == username.Trim());
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> GetUserById(Guid NidUser, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Users.Include(p => p.Carts).ThenInclude(p => p.Product).Include(p => p.Favorites).ThenInclude(p => p.Product).Include(p => p.Orders).ThenInclude(p => p.Ships).Include(p => p.Orders).ThenInclude(p => p.OrderDetails).ThenInclude(p => p.Product).FirstOrDefaultAsync(p => p.Id == NidUser) ?? new User();
                else
                    return await _context.Users.FirstOrDefaultAsync(p => p.Id == NidUser) ?? new User();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public async Task<User> GetUserByUsername(string Username, bool IncludeAll = false)
        {
            try
            {
                if (_context.Users.Any(p => p.Username.Trim() == Username.Trim() && p.IsAdmin == false))
                {
                    var User = await _context.Users.FirstOrDefaultAsync(p => p.Username.Trim() == Username.Trim());
                    if (User != null)
                        return User;
                    else
                        return new User();
                }
                else
                    return new User();
            }
            catch (Exception)
            {
                return new User();
            }
        }

        public async Task<User> LoginWithUsername(string username, string password)
        {
            try
            {
                if (username.Trim() != "anonymous")
                {
                    if (_context.Users.Any(p => p.Username.Trim() == username.Trim() && p.IsAdmin == false))
                    {
                        var user = await _context.Users.FirstOrDefaultAsync(p => p.Username.Trim() == username.Trim());
                        if (user != null)
                        {
                            if (user.Password == password)
                            {
                                user.LastLoginDate = DateTime.Now;
                                _context.Update(user);
                                await _context.SaveChangesAsync();
                                return user;
                            }
                            else
                                return new User();
                        }
                        else
                            return new User();
                    }
                    else
                        return new User();
                }
                else
                    return new User();
            }
            catch (Exception)
            {
                return new User();
            }
        }
    }
}
