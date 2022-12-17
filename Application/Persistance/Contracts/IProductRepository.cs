using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IProductRepository
    {
        Task<bool> CreateProduct(Product item);
        Task<bool> UpdateProduct(Product item);
        Task<bool> DeleteProduct(Guid Id);
        Task<Product> GetProduct(Guid NidProduct);
        //to process actions
        //IEnumerable<Comment> GetComments(int State = 0, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Ship> GetShips(int State = 0, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Order> GetOrders(int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Product> GetProducts(bool IncludeAll = true, int State = 0, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Product> GetProductsByCategory(Guid NidCategory, Guid NidType, Guid NidBrand, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Product> GetProductsByCreateDate(DateTime From, DateTime To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Product> GetProductsByPriceRange(decimal From, decimal To, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //IEnumerable<Product> GetProductsByAvailablity(int AvailableCount, int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        //Comment GetComment(Guid NidComment);
        //Order GetOrder(Guid NidOrder);
        //Ship GetShip(Guid NidShip);
        //bool UpdateComment(Comment item);
        //bool UpdateOrder(Order item);
        //bool UpdateOrderDetail(OrderDetail item);
        //bool UpdateShip(Ship item);
    }
}
