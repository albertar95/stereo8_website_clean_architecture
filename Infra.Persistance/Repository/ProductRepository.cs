using Application.Model;
using Application.Persistance.Contracts;
using Domain;
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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        private readonly AudioShopDbContext _context;

        public ProductRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteProduct(Guid Id)
        {
            var product = _context.Products.FirstOrDefault(b => b.Id == Id);
            if (product != null)
            {
                product.State = 1;
                _context.Entry(product).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<Product> GetProduct(Guid NidProduct, bool IncludeAll = true)
        {
            if(IncludeAll)
                return await _context.Products.Include(p => p.Category).Include(p => p.Type).Include(p => p.Brand).Include(p => p.User).Include(p => p.Comments).ThenInclude(q => q.User).FirstOrDefaultAsync(b => b.Id == NidProduct) ?? null!;
            else
                return await _context.Products.FirstOrDefaultAsync(b => b.Id == NidProduct) ?? null!;
        }

        public async Task<IEnumerable<Product>> GetProducts(bool IncludeAll = true, int State = 0, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                else
                    return await _context.Products.Where(p => p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(Guid NidCategory, Guid NidType, Guid NidBrand, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                {
                    if (NidType == Guid.Empty && NidBrand == Guid.Empty)
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CategoryId == NidCategory && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType != Guid.Empty && NidBrand == Guid.Empty)
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CategoryId == NidCategory && p.State == State && p.TypeId == NidType).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType == Guid.Empty && NidBrand != Guid.Empty)
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CategoryId == NidCategory && p.State == State && p.BrandId == NidBrand).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType != Guid.Empty && NidBrand != Guid.Empty)
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CategoryId == NidCategory && p.State == State && p.TypeId == NidType && p.BrandId == NidBrand).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CategoryId == NidCategory && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                }
                else
                {
                    if (NidType == Guid.Empty && NidBrand == Guid.Empty)
                        return await _context.Products.Where(p => p.CategoryId == NidCategory && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType != Guid.Empty && NidBrand == Guid.Empty)
                        return await _context.Products.Where(p => p.CategoryId == NidCategory && p.State == State && p.TypeId == NidType).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType == Guid.Empty && NidBrand != Guid.Empty)
                        return await _context.Products.Where(p => p.CategoryId == NidCategory && p.State == State && p.BrandId == NidBrand).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else if (NidType != Guid.Empty && NidBrand != Guid.Empty)
                        return await _context.Products.Where(p => p.CategoryId == NidCategory && p.State == State && p.TypeId == NidType && p.BrandId == NidBrand).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else
                        return await _context.Products.Where(p => p.CategoryId == NidCategory && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                }

            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCreateDate(DateTime From, DateTime To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.CreateDate >= From && p.CreateDate <= To && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                else
                    return await _context.Products.Where(p => p.CreateDate >= From && p.CreateDate <= To && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByPriceRange(decimal From, decimal To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.Price >= From && p.Price <= To && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                else
                    return await _context.Products.Where(p => p.Price >= From && p.Price <= To && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByAvailablity(int AvailableCount, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                {
                    if (AvailableCount == 0)
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.AvailableCount == 0 && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else
                        return await _context.Products.Include(p => p.Category).Include(q => q.Type).Include(x => x.Brand).Where(p => p.AvailableCount >= AvailableCount && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                }
                else
                {
                    if (AvailableCount == 0)
                        return await _context.Products.Where(p => p.AvailableCount == 0 && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                    else
                        return await _context.Products.Where(p => p.AvailableCount >= AvailableCount && p.State == State).OrderByDescending(q => q.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                }

            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<Product> GetProductByTitle(string Title, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p => p.Type).Include(p => p.Comments.Where(q => q.State == 1)).ThenInclude(p => p.User).FirstOrDefaultAsync(q => q.ProductName == Title && q.State == 0) ?? new Product();
                else
                    return await _context.Products.FirstOrDefaultAsync(q => q.ProductName == Title && q.State == 0) ?? new Product();
            }
            catch (Exception)
            {
                return new Product();
            }
        }

        public async Task<int> GetProductCount(Guid NidCategory)
        {
            try
            {
                return await _context.Products.CountAsync(p => p.CategoryId == NidCategory);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Product>> GetSpecialProducts(int Pagesize = 10, int Skip = 0)
        {
            try
            {
                var cats = await _context.Products.Where(p => p.State == 0 && p.Priority >= 4).GroupBy(p => p.CategoryId).Select(p => new { catId = p.Key }).ToListAsync();
                List<Product> result = new List<Product>();
                foreach (var c in cats)
                {
                    foreach (var prd in _context.Products.Include(p => p.Category).Include(p => p.Brand).Where(p => p.State == 0 && p.Priority >= 4 && p.CategoryId == c.catId).OrderByDescending(q => q.CreateDate).Skip(Skip * Pagesize).Take(Pagesize).ToList())
                    {
                        result.Add(prd);
                    }
                }
                return result;
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetOffProducts(int Pagesize = 10, int Skip = 0)
        {
            try
            {
                return await _context.Products.Include(p => p.Brand).Where(p => p.State == 0).OrderByDescending(q => q.OffPercentage).Skip(Skip * Pagesize).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid NidCategory, int Pagesize = 10, int Skip = 0)
        {
            try
            {
                return await _context.Products.Where(p => p.State == 0 && p.CategoryId == NidCategory).OrderByDescending(q => q.CreateDate).Skip(Skip * Pagesize).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetFilteredProducts(UIProductFilters filters)
        {
            try
            {
                switch (filters.SortType)
                {
                    case 1:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderByDescending(q => q.Rating).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderByDescending(q => q.Rating).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.Rating).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.Rating).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                    case 2:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                    case 3:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderBy(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderBy(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderBy(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderBy(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                    case 4:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderByDescending(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderByDescending(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.Price).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                    case 5:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderBy(q => q.ProductName).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderBy(q => q.ProductName).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderBy(q => q.ProductName).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderBy(q => q.ProductName).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                    default:
                        if (!filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && !filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (!filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else if (filters.TypeIds.Any() && filters.BrandIds.Any())
                            return await _context.Products.Where(p => p.State == 0 && p.CategoryId == filters.NidCategory && p.Price >= filters.MinPrice && p.Price <= filters.MaxPrice && filters.TypeIds.Contains(p.TypeId) && filters.BrandIds.Contains(p.BrandId)).OrderByDescending(q => q.CreateDate).Skip(filters.Skip * filters.Pagesize).Take(filters.Pagesize).ToListAsync();
                        else
                            return new List<Product>();
                }
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> GetSimilarProducts(Guid NidProduct)
        {
            try
            {
                var tmpProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == NidProduct) ?? new Product();
                if (tmpProduct.Id != Guid.Empty)
                    return await _context.Products.Where(p => p.Id != tmpProduct.Id && p.BrandId == tmpProduct.BrandId && p.TypeId == tmpProduct.TypeId && p.Price >= (tmpProduct.Price - (tmpProduct.Price * (decimal)0.2)) && p.Price <= (tmpProduct.Price + (tmpProduct.Price * (decimal)0.2))).Take(5).ToListAsync();
                else
                    return new List<Product>();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<IEnumerable<Product>> SearchProduct(string input)
        {
            List<Product> result = new List<Product>();
            foreach (var sr in await _context.Products.Where(p => p.ProductName.Contains(input)).Take(3).ToListAsync())
            {
                result.Add(sr);
            }
            return result;
        }

        public async Task<IEnumerable<Product>> GetBargainedProducts(int Pagesize = 10, int Skip = 0)
        {
            try
            {
                return await _context.Products.Include(p => p.Category).Include(p => p.Brand).Include(p => p.Type).Where(p => p.State == 0 && p.OffPercentage > 0).OrderByDescending(q => q.CreateDate).Skip(Skip * Pagesize).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Product>();
            }
        }

        public async Task<int> GetBargainProductCount()
        {
            try
            {
                return await _context.Products.CountAsync(p => p.OffPercentage != 0 && p.State == 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
