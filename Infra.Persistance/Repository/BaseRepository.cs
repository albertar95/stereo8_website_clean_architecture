using Application.Persistance.Contracts.Common;
using Infra.Persistance.Context;
using Infra.Persistance.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly AudioShopDbContext _context;

        public BaseRepository(AudioShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create<T>(T item)
        {
            //item.Id = Guid.NewGuid();
            //item.State = 0;
            //item.CreateDate = DateTime.Now;
            //item.PersianCreateDate = Commons.GetPersianDate(DateTime.Now);
            if (item == null)
                return false;
            else
            {
                _context.Add(item);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Delete<T>(T item)
        {
            if (item == null)
                return false;
            else
            {
                _context.Remove(item);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
        }

        public async Task<bool> Update<T>(T item)
        {
            if (item == null)
                return false;
            else
            {
                _context.Update(item);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}
