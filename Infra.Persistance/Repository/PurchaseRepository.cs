using Application.Persistance.Contracts;
using Domain;
using Infra.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Repository
{
    public class PurchaseRepository : BaseRepository, IPurchaseRepository
    {
        private readonly AudioShopDbContext _context;

        public PurchaseRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DeleteCart(Guid Id)
        {
            var cart = _context.Carts.FirstOrDefault(b => b.Id == Id);
            if (cart != null)
            {
                _context.Remove(cart);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<bool> DeleteFavorite(Guid Id)
        {
            var fav = _context.Favorites.FirstOrDefault(b => b.Id == Id);
            if (fav != null)
            {
                _context.Remove(fav);
                if (await _context.SaveChangesAsync() == 1)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public async Task<decimal> GetCartAggregateByUserId(Guid NidUser)
        {
            try
            {
                return await _context.Carts.Where(p => p.UserId == NidUser).SumAsync(p => p.Product.Price * p.Quantity);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<Cart> GetCartById(Guid NidCart)
        {
            try
            {
                return await _context.Carts.FirstOrDefaultAsync(p => p.Id == NidCart) ?? new Cart();
            }
            catch (Exception)
            {
                return new Cart();
            }
        }

        public async Task<Cart> GetCartByProductId(Guid NidUser, Guid NidProduct)
        {
            return await _context.Carts.FirstOrDefaultAsync(p => p.ProductId == NidProduct && p.UserId == NidUser) ?? new Cart();
        }

        public async Task<int> GetCartCountByUserId(Guid NidUser)
        {
            try
            {
                return await _context.Carts.Where(p => p.UserId == NidUser).CountAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Guid>> GetCartProductsByUserId(Guid NidUser)
        {
            return await _context.Carts.Where(p => p.UserId == NidUser).Select(q => q.ProductId).ToListAsync();
        }

        public async Task<IEnumerable<Cart>> GetCartsByUserId(Guid NidUser, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Carts.Include(p => p.Product).Where(p => p.UserId == NidUser).ToListAsync() ?? new List<Cart>();
                else
                    return await _context.Carts.Where(p => p.UserId == NidUser).ToListAsync() ?? new List<Cart>();
            }
            catch (Exception)
            {
                return new List<Cart>();
            }
        }

        public async Task<Favorite> GetFavoriteById(Guid NidFav)
        {
            try
            {
                return await _context.Favorites.FirstOrDefaultAsync(p => p.Id == NidFav) ?? new Favorite();
            }
            catch (Exception)
            {
                return new Favorite();
            }
        }

        public async Task<Favorite> GetFavoriteByProductId(Guid NidUser, Guid NidProduct)
        {
            return await _context.Favorites.FirstOrDefaultAsync(p => p.ProductId == NidProduct && p.UserId == NidUser) ?? new Favorite();
        }

        public async Task<int> GetFavoriteCountByUserId(Guid NidUser)
        {
            try
            {
                return await _context.Favorites.Where(p => p.UserId == NidUser).CountAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Guid>> GetFavoriteProductsByUserId(Guid NidUser)
        {
            return await _context.Favorites.Where(p => p.UserId == NidUser).Select(q => q.ProductId).ToListAsync();
        }

        public async Task<IEnumerable<Favorite>> GetFavoritesByUserId(Guid NidUser, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Favorites.Include(p => p.Product).Where(p => p.UserId == NidUser).ToListAsync() ?? new List<Favorite>();
                else
                    return await _context.Favorites.Where(p => p.UserId == NidUser).ToListAsync() ?? new List<Favorite>();
            }
            catch (Exception)
            {
                return new List<Favorite>();
            }
        }

        public async Task<Order> GetOrder(Guid NidOrder)
        {
            try
            {
                return await _context.Orders.Include(q => q.Ships).Include(q => q.User).Include(q => q.OrderDetails).ThenInclude(q => q.Product).FirstOrDefaultAsync(p => p.Id == NidOrder) ?? new Order();
            }
            catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<Order> GetOrderById(Guid NidOrder, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Orders.Include(p => p.Ships).Include(p => p.User).FirstOrDefaultAsync(p => p.Id == NidOrder) ?? new Order();
                else
                    return _context.Orders.FirstOrDefault(p => p.Id == NidOrder) ?? new Order();
            }
            catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<Order> GetOrderByUserId(Guid NidUser, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Orders.Include(p => p.Ships).Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == NidUser && p.State == 0) ?? new Order();
                else
                    return await _context.Orders.FirstOrDefaultAsync(p => p.UserId == NidUser && p.State == 0) ?? new Order();
            }
            catch (Exception)
            {
                return new Order();
            }
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(Guid NidOrder, bool IncludeAll = true)
        {
            try
            {
                return await _context.OrderDetails.Where(p => p.OrderId == NidOrder).ToListAsync() ?? new List<OrderDetail>();
            }
            catch (Exception)
            {
                return new List<OrderDetail>();
            }
        }

        public async Task<IEnumerable<Order>> GetOrders(int State = 0, bool IncludeAll = true, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Orders.Include(q => q.Ships).Include(q => q.OrderDetails).Include(q => q.User).Where(p => p.State == State).OrderByDescending(x => x.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
                else
                    return await _context.Orders.Where(p => p.State == State).OrderByDescending(x => x.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Order>();
            }
        }

        public async Task<Ship> GetShip(Guid NidShip)
        {
            try
            {
                return await _context.Ships.FirstOrDefaultAsync(p => p.Id == NidShip) ?? new Ship();
            }
            catch (Exception)
            {
                return new Ship();
            }
        }

        public async Task<Ship> GetShipByOrderId(Guid NidOrder, bool IncludeAll = true)
        {
            try
            {
                if (IncludeAll)
                    return await _context.Ships.Include(p => p.Order).FirstOrDefaultAsync(p => p.OrderId == NidOrder) ?? new Ship();
                else
                    return await _context.Ships.FirstOrDefaultAsync(p => p.OrderId == NidOrder) ?? new Ship();
            }
            catch (Exception)
            {
                return new Ship();
            }
        }

        public async Task<IEnumerable<ShipPrice>> GetShipPrices()
        {
            try
            {
                return await _context.ShipPrices.ToListAsync() ?? new List<ShipPrice>();
            }
            catch (Exception)
            {
                return new List<ShipPrice>();
            }
        }

        public async Task<IEnumerable<Ship>> GetShips(int State = 0, int Pagesize = 100, int Skip = 0)
        {
            try
            {
                return await _context.Ships.Include(q => q.Order).ThenInclude(x => x.User).Where(p => p.State == State).OrderByDescending(p => p.CreateDate).Skip(Skip).Take(Pagesize).ToListAsync();
            }
            catch (Exception)
            {
                return new List<Ship>();
            }
        }

        public async Task<bool> UpdateAvailableCount(Guid NidProduct, int Quantity)
        {
            try
            {
                int currentCount = 0;
                var tmpProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == NidProduct);
                if (tmpProduct != null)
                    currentCount = tmpProduct.AvailableCount;
                if (currentCount > 0)
                {
                    if (tmpProduct.AvailableCount >= Quantity)
                        tmpProduct.AvailableCount = tmpProduct.AvailableCount - Quantity;
                    else
                        tmpProduct.AvailableCount = 0;
                    return await Update(tmpProduct);
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
