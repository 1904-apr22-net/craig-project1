using System;
using System.Collections.Generic;
using System.Linq;

namespace SkateShop.Library.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public string Address{ get; set; }
        public string City{ get; set; }
        public string State{ get; set; }
        public string Country{ get; set; }

        public List<Order> Orders{ get; set; } = new List<Order>();
        public List<Customer> Customers{ get; set; } = new List<Customer>();
        public List<InventoryItem> Inventory{ get; set; } = new List<InventoryItem>();
    }
}
