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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        private readonly AudioShopDbContext _context;

        public CategoryRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteBrand(Guid Id)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Id == Id);
            if (brand != null)
            {
                brand.State = 1;
                _context.Entry(brand).State = EntityState.Modified;
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
                _context.Entry(category).State = EntityState.Modified;
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
                _context.Entry(type).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).ToListAsync();
        }

        public async Task<Brand> GetBrand(Guid NidBrand)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.Id == NidBrand) ?? null!;
        }

        public async Task<IEnumerable<Category>> GetCategories(int State = 0, bool IncludeProducts = false)
        {
            if(IncludeProducts)
                return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).Include(p => p.Products.Where(q => q.State == 0)).Where(p => p.State == State).ToListAsync();
            else
                return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).Where(p => p.State == State).ToListAsync();
        }

        public async Task<Category> GetCategory(Guid NidCategory, bool IncludeProducts = false)
        {
            if(IncludeProducts)
                return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).Include(p => p.Products.Where(q => q.State == 0)).FirstOrDefaultAsync(b => b.Id == NidCategory) ?? null!;
            else
                return await _context.Categories.FirstOrDefaultAsync(b => b.Id == NidCategory) ?? null!;
        }

        public async Task<Category> GetCategoryByTitle(string Title, bool IncludeProducts = false)
        {
            if (IncludeProducts)
                return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).Include(p => p.Products.Where(q => q.State == 0)).FirstOrDefaultAsync(b => b.CategoryName == Title && b.State == 0) ?? null!;
            else
                return await _context.Categories.Include(p => p.Brands.Where(q => q.State == 0)).Include(p => p.Types.Where(q => q.State == 0)).FirstOrDefaultAsync(b => b.CategoryName == Title && b.State == 0) ?? null!;
        }

        public async Task<Domain.Type> GetType(Guid NidType)
        {
            return await _context.Types.FirstOrDefaultAsync(b => b.Id == NidType) ?? null!;
        }
    }
}
