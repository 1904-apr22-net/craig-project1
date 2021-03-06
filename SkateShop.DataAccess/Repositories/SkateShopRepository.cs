using System;
using Microsoft.EntityFrameworkCore;
using SkateShop.Library.Interfaces;
using SkateShop.Library.Models;
using SkateShop.DataAccess.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SkateShop.DataAccess.Repositories
{
    public class SkateShopRepository : ISkateShopRepository
    {
        private readonly SkateShopDbContext _dbContext;

        public SkateShopRepository(SkateShopDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        
        public IEnumerable<Library.Models.Location> GetLocations()
        {
            IQueryable<Entities.Location> items = _dbContext.Location
                .Include(i => i.InventoryItem)
                    .ThenInclude(p => p.Product)
                .Include(o => o.Order)
                    .ThenInclude(oi => oi.OrderItem)
                        .ThenInclude(p => p.Product)
                .Include(c => c.Customer)
                    .ThenInclude(o => o.Order)
                        .ThenInclude(oi => oi.OrderItem)
                            .ThenInclude(p => p.Product);
            return Mapper.Map(items);
        }
        public IEnumerable<Library.Models.Order> GetOrders()
        {
            IQueryable<Entities.Order> items = _dbContext.Order
                .Include(c => c.Customer)
                .Include(l => l.Location);
            return Mapper.Map(items);
        }
        public IEnumerable<Library.Models.Customer> GetCustomers()
        {
            IQueryable<Entities.Customer> items = _dbContext.Customer
                .Include(o => o.Order)
                    .ThenInclude(oi => oi.OrderItem)
                        .ThenInclude(p => p.Product);
            return Mapper.Map(items);
        }
        public IEnumerable<Library.Models.Product> GetProducts()
        {
            IQueryable<Entities.Product> items = _dbContext.Product
                .Include(i => i.InventoryItem)
                .Include(o => o.OrderItem);
            return Mapper.Map(items);
        }

        public Library.Models.Location GetLocationById(int id)
        {
            Entities.Location location = _dbContext.Location.Include(o => o.Order)
                .AsNoTracking().First(l => l.LocationId == id);
            return Mapper.Map(location);
        }

        public Library.Models.Customer GetCustomerById(int id)
        {
            Entities.Customer customer = _dbContext.Customer.Include(o => o.Order)
                .AsNoTracking().First(c => c.CustomerId == id);
            return Mapper.Map(customer);
        }

        public Library.Models.Order GetOrderById(int id)
        {
            Entities.Order order = _dbContext.Order.Include(o => o.OrderItem).ThenInclude(p => p.Product)
                .AsNoTracking().First(o => o.OrderId == id);
            return Mapper.Map(order);
        }

        public IEnumerable<Library.Models.Order> SortOrderHistoryByCheapest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderBy(t => t.Total)).ToList();
        public IEnumerable<Library.Models.Order> SortOrderHistoryByMostExpensive(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderByDescending(t => t.Total));
        public IEnumerable<Library.Models.Order> SortOrderHistoryByEarliest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderBy(t => t.Time));
        public IEnumerable<Library.Models.Order> SortOrderHistoryByLatest(int id) =>
            Mapper.Map(_dbContext.Location.Find(id).Order.OrderByDescending(t => t.Time));

        public IEnumerable<Library.Models.Order> GetCustomerOrderHistory(int id) =>
            Mapper.Map(_dbContext.Customer.Find(id).Order);

        public void PlaceOrder(Library.Models.Order order, List<Library.Models.Product> cart)
        {
            Entities.Order newOrder = Mapper.Map(order);
            _dbContext.Add(newOrder);
            Save();
            var latestOrder = _dbContext.Order.OrderByDescending(x => x.OrderId).First();
            for (var i=0; i<cart.Count; i++)
            {
                Entities.OrderItem newOI = new Entities.OrderItem();
                newOI.ProductId = cart[i].ProductId;
                newOI.OrderId = latestOrder.OrderId;
                _dbContext.OrderItem.Add(newOI);
            }

            Save();
        }

        public void Save()
        {//comment
            _dbContext.SaveChanges();
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}