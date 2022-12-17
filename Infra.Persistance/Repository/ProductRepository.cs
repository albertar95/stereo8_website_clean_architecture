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
    public class ProductRepository : IProductRepository
    {
        private readonly AudioShopDbContext _context;

        public ProductRepository(AudioShopDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProduct(Product item)
        {
            _context.Products.Add(item);
            if (await _context.SaveChangesAsync() == 1)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteProduct(Guid Id)
        {
            var product = _context.Products.FirstOrDefault(b => b.Id == Id);
            if (product != null)
            {
                product.State = 1;
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<Product> GetProduct(Guid NidProduct)
        {
            return await _context.Products.FirstOrDefaultAsync(b => b.Id == NidProduct) ?? null!;
        }

        public async Task<bool> UpdateProduct(Product item)
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
