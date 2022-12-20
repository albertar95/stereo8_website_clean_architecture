using Application.DTO.Brand;
using Application.DTO.Category;
using Application.DTO.Type;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface ICategoryRepository
    {
        Task<bool> CreateBrand(Brand item);
        Task<bool> CreateCategory(Category item);
        Task<bool> CreateType(Domain.Type item);
        Task<bool> UpdateBrand(Brand item);
        Task<bool> UpdateCategory(Category item);
        Task<bool> UpdateType(Domain.Type item);
        Task<bool> DeleteBrand(Guid Id);
        Task<bool> DeleteCategory(Guid Id);
        Task<bool> DeleteType(Guid  Id);
        Task<Category> GetCategory(Guid NidCategory);
        Task<Brand> GetBrand(Guid NidBrand);
        Task<Domain.Type> GetType(Guid NidType);
        // to process actions
        Task<IEnumerable<Category>> GetCategories(int State = 0);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
