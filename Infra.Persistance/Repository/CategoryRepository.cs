using Application.DTO.Brand;
using Application.DTO.Category;
using Application.DTO.Type;
using Application.Persistance.Contracts;
using Domain;
using Infra.Persistance.Context;
using Infra.Persistance.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AudioShopDbContext _context;

        public CategoryRepository(AudioShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateBrand(Brand item)
        {
            item.Id = Guid.NewGuid();
            item.State = 0;
            item.CreateDate = DateTime.Now;
            item.PersianCreateDate = Commons.GetPersianDate(DateTime.Now);
            _context.Brands.Add(item);
            if(await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> CreateCategory(Category item)
        {
            item.Id = Guid.NewGuid();
            item.State = 0;
            item.CreateDate = DateTime.Now;
            item.PersianCreateDate = Commons.GetPersianDate(DateTime.Now);
            _context.Categories.Add(item);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> CreateType(Domain.Type item)
        {
            item.Id = Guid.NewGuid();
            item.State = 0;
            item.CreateDate = DateTime.Now;
            item.PersianCreateDate = Commons.GetPersianDate(DateTime.Now);
            _context.Types.Add(item);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteBrand(Guid Id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == Id);
            if (brand != null)
            {
                brand.State = 1;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> DeleteCategory(Guid Id)
        {
            var category = _context.Categories.FirstOrDefault(b => b.Id == Id);
            if (category != null)
            {
                category.State = 1;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> DeleteType(Guid Id)
        {
            var type = _context.Categories.FirstOrDefault(b => b.Id == Id);
            if (type != null)
            {
                type.State = 1;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<Brand> GetBrand(Guid NidBrand)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == NidBrand) ?? null!;
        }

        public async Task<Category> GetCategory(Guid NidCategory)
        {
            return await _context.Categories.FirstOrDefaultAsync(b => b.Id == NidCategory) ?? null!;
        }

        public async Task<Domain.Type> GetType(Guid NidType)
        {
            return await _context.Types.FirstOrDefaultAsync(b => b.Id == NidType) ?? null!;
        }

        public async Task<bool> UpdateBrand(Brand item)
        {
            item.LastModified = DateTime.Now;
            item.PersianLastModified = Commons.GetPersianDate(DateTime.Now);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateCategory(Category item)
        {
            item.LastModified = DateTime.Now;
            item.PersianLastModified = Commons.GetPersianDate(DateTime.Now);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> UpdateType(Domain.Type item)
        {
            item.LastModified = DateTime.Now;
            item.PersianLastModified = Commons.GetPersianDate(DateTime.Now);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }
    }
}
