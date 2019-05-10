using SkateShop.Library.Models;
using System;
using System.Collections.Generic;

namespace SkateShop.Library.Interfaces
{
    public interface ISkateShopRepository : IDisposable
    {
        IEnumerable<Location> GetLocations();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Product> GetProducts();
        Customer GetCustomerById(int id);
        Location GetLocationById(int id);
        Order GetOrderById(int id);
        IEnumerable<Order> GetCustomerOrderHistory(int id);
        IEnumerable<Order> SortOrderHistoryByCheapest(int id);
        IEnumerable<Order> SortOrderHistoryByMostExpensive(int id);
        IEnumerable<Order> SortOrderHistoryByEarliest(int id);
        IEnumerable<Order> SortOrderHistoryByLatest(int id);
        void PlaceOrder(Order order, List<Product> cart);
    }
}
