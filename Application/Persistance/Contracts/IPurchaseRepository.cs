using Application.Persistance.Contracts.Common;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistance.Contracts
{
    public interface IPurchaseRepository : IBaseRepository
    {
        Task<IEnumerable<Ship>> GetShips(int State = 0, int Pagesize = 100, int Skip = 0);
        Task<IEnumerable<Order>> GetOrders(int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0);
        Task<Order> GetOrder(Guid NidOrder);
        Task<Ship> GetShip(Guid NidShip);
        //cart
        Task<IEnumerable<Cart>> GetCartsByUserId(Guid NidUser, bool IncludeAll = true);
        Task<IEnumerable<Guid>> GetCartProductsByUserId(Guid NidUser);
        Task<int> GetCartCountByUserId(Guid NidUser);
        Task<decimal> GetCartAggregateByUserId(Guid NidUser);
        Task<Cart> GetCartById(Guid NidCart);
        Task<Cart> GetCartByProductId(Guid NidUser, Guid NidProduct);
        //fav
        Task<IEnumerable<Favorite>> GetFavoritesByUserId(Guid NidUser, bool IncludeAll = true);
        Task<IEnumerable<Guid>> GetFavoriteProductsByUserId(Guid NidUser);
        Task<int> GetFavoriteCountByUserId(Guid NidUser);
        Task<Favorite> GetFavoriteById(Guid NidFav);
        Task<Favorite> GetFavoriteByProductId(Guid NidUser, Guid NidProduct);
        //checkout
        Task<Order> GetOrderByUserId(Guid NidUser, bool IncludeAll = true);
        Task<Ship> GetShipByOrderId(Guid NidOrder, bool IncludeAll = true);
        Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid NidOrder, bool IncludeAll = true);
        Task<IEnumerable<ShipPrice>> GetShipPrices();
        Task<Order> GetOrderById(Guid NidOrder, bool IncludeAll = true);
        Task<bool> UpdateAvailableCount(Guid NidProduct, int Quantity);
        //deletes
        Task<bool> DeleteCart(Guid Id);
        Task<bool> DeleteFavorite(Guid Id);
    }
}
