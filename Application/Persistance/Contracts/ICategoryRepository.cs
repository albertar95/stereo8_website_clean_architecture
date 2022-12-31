using Application.DTO.Brand;
using Application.DTO.Category;
using Application.DTO.Type;
using Application.Persistance.Contracts.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface ICategoryRepository : IBaseRepository
    {
        Task<bool> DeleteBrand(Guid Id);
        Task<bool> DeleteCategory(Guid Id);
        Task<bool> DeleteType(Guid  Id);
        Task<Category> GetCategory(Guid NidCategory, bool IncludeProducts = false);
        Task<Category> GetCategoryByTitle(string Title, bool IncludeProducts = false);
        Task<Brand> GetBrand(Guid NidBrand);
        Task<Domain.Type> GetType(Guid NidType);
        Task<IEnumerable<Category>> GetCategories(int State = 0,bool IncludeProducts = false);
        Task<IEnumerable<Category>> GetAllCategories();
    }
}
