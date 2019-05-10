﻿using System;
using System.Collections.Generic;

namespace SkateShop.DataAccess.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Location Location { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
