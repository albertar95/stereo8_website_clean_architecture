using Application.Model;
using Application.Persistance.Contracts.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IProductRepository : IBaseRepository
    {
        Task<bool> DeleteProduct(Guid Id);
        Task<Product> GetProduct(Guid NidProduct,bool IncludeAll = true);
        Task<Product> GetProductByTitle(string Title, bool IncludeAll = true);
        Task<int> GetProductCount(Guid NidCategory);
        Task<int> GetBargainProductCount();
        Task<IEnumerable<Product>> GetProducts(bool IncludeAll = true, int State = 0, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Product>> GetProductsByCategory(Guid NidCategory, Guid NidType, Guid NidBrand, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Product>> GetProductsByCreateDate(DateTime From, DateTime To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Product>> GetProductsByPriceRange(decimal From, decimal To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Product>> GetProductsByAvailablity(int AvailableCount, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Product>> GetSpecialProducts(int Pagesize = 10, int Skip = 0);
        Task<IEnumerable<Product>> GetOffProducts(int Pagesize = 10, int Skip = 0);
        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid NidCategory, int Pagesize = 10, int Skip = 0);
        Task<IEnumerable<Product>> GetFilteredProducts(UIProductFilters filters);
        Task<IEnumerable<Product>> GetSimilarProducts(Guid NidProduct);
        Task<IEnumerable<Product>> SearchProduct(string input);
        Task<IEnumerable<Product>> GetBargainedProducts(int Pagesize = 10, int Skip = 0);
        //to process actions
        //IEnumerable<Comment> GetComments(int State = 0, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Ship> GetShips(int State = 0, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Order> GetOrders(int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //Comment GetComment(Guid NidComment);
        //Order GetOrder(Guid NidOrder);
        //Ship GetShip(Guid NidShip);
        //bool UpdateComment(Comment item);
        //bool UpdateOrder(Order item);
        //bool UpdateOrderDetail(OrderDetail item);
        //bool UpdateShip(Ship item);
    }
}
