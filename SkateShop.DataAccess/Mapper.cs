using System;
using System.Collections.Generic;
using System.Linq;
using SkateShop;

namespace SkateShop.DataAccess
{
    public static class Mapper
    {
        public static Library.Models.Location Map(Entities.Location location) => new Library.Models.Location
        {
            LocationId = location.LocationId,
            Address = location.Address,
            City = location.City,
            State = location.State,
            Country = location.Country,
            Orders = Map(location.Order).ToList(),
            Customers = Map(location.Customer).ToList(),
            Inventory = Map(location.InventoryItem).ToList()
        };

        public static Library.Models.Order Map(Entities.Order order) => new Library.Models.Order
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            LocationId = order.LocationId,
            Time = order.Time,
            Quantity = order.Quantity,
            Total = order.Total,
            Products = Map(order.OrderItem).ToList()
        };

        public static Entities.Order Map(Library.Models.Order order) => new Entities.Order
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            LocationId = order.LocationId,
            Time = order.Time,
            Quantity = order.Quantity,
            Total = order.Total,
        };

        public static Library.Models.Product Map(Entities.OrderItem orderItem) =>
            Map(orderItem.Product);


        public static Library.Models.Product Map(Entities.Product product) => new Library.Models.Product
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = (double)product.Price
        };

        public static Entities.Product Map(Library.Models.Product product) => new Entities.Product
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = (decimal)product.Price
        };

        public static Library.Models.Customer Map(Entities.Customer customer) => new Library.Models.Customer
        {
            CustomerId = customer.CustomerId,
            LocationId = customer.LocationId,
            Address = customer.Address,
            City = customer.City,
            State = customer.State,
            Country = customer.Country,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Orders = Map(customer.Order).ToList()
        };

        public static Library.Models.InventoryItem Map(Entities.InventoryItem inventoryItem) => new Library.Models.InventoryItem
        {
            InventoryItemId = inventoryItem.InventoryItemId,
            LocationId = inventoryItem.LocationId,
            ProductId = inventoryItem.ProductId,
            Quantity = inventoryItem.Quantity
        };

        public static IEnumerable<Library.Models.Location> Map(IEnumerable<Entities.Location> locations) =>
            locations.Select(Map);

        public static IEnumerable<Library.Models.Order> Map(IEnumerable<Entities.Order> orders) =>
            orders.Select(Map);

        public static IEnumerable<Library.Models.Customer> Map(IEnumerable<Entities.Customer> customers) =>
            customers.Select(Map);

        public static IEnumerable<Library.Models.InventoryItem> Map(IEnumerable<Entities.InventoryItem> inventory) =>
            inventory.Select(Map);

        public static IEnumerable<Library.Models.Product> Map(IEnumerable<Entities.OrderItem> orderItems) =>
            orderItems.Select(Map);

        public static IEnumerable<Library.Models.Product> Map(IEnumerable<Entities.Product> products) =>
            products.Select(Map);
        
    }
}