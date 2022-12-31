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
    public class GeneralRepository : BaseRepository, IGeneralRepository
    {
        private readonly AudioShopDbContext _context;

        public GeneralRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteComment(Guid Id)
        {
            var comment = _context.Comments.FirstOrDefault(b => b.Id == Id);
            if (comment != null)
            {
                comment.State = 1;
                _context.Entry(comment).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> DeleteSetting(Guid Id)
        {
            var setting = _context.Settings.FirstOrDefault(b => b.Id == Id);
            if (setting != null)
            {
                _context.Remove(setting);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<Comment> GetComment(Guid NidComment)
        {
            try
            {
                return await _context.Comments.FirstOrDefaultAsync(p => p.Id == NidComment) ?? new Comment();
            }
            catch (Exception)
            {
                return new Comment();
            }
        }

        public async Task<IEnumerable<Comment>> GetComments(int State = 0, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                return await _context.Comments.Include(q => q.User).Where(p => p.State == State).OrderByDescending(p => p.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Comment>();
            }
        }

        public string[] GetIndexPageValues()
        {
            string[] result = new string[4];
            result[0] = _context.Products.Count(p => p.State == 0).ToString();
            if (_context.Orders.Any())
                result[1] = String.Format("{0:n0} تومان", _context.Orders.Where(p => p.State == 101 || p.State == 100).Sum(p => p.TotalPrice));
            else
                result[1] = String.Format("{0:n0} تومان", 0);
            if (_context.Ships.Any())
                result[2] = String.Format("{0}", ((_context.Ships.Count(p => p.State == 3)) / (_context.Ships.Count())) * 100);
            else
                result[2] = String.Format("{0}", 0);
            result[3] = _context.Comments.Count(p => p.State == 0).ToString();
            return result;
        }

        public async Task<Setting> GetSetting(Guid NidSetting, byte State = 0)
        {
            try
            {
                return await _context.Settings.FirstOrDefaultAsync(p => p.Id == NidSetting && p.State == State) ?? new Setting(); ;
            }
            catch (Exception)
            {
                return new Setting();
            }
        }

        public async Task<IEnumerable<Setting>> GetSettings(int Pagesize = 100, int Skip = 0, int State = 0)
        {
            try
            {
                return await _context.Settings.Where(p => p.State == State).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Setting>();
            }
        }
    }
}
