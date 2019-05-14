using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkateShop.WebUI.Models
{
    public class CustomerViewModel
    {
        [Display(Name = "ID")]
        public int CustomerId;

        public int LocationId;

        public string Address;

        [Display(Name = "First Name")]
        public string FirstName;

        [Display(Name = "Last Name")]
        public string LastName;

        public IEnumerable<OrderViewModel> Orders;
    }
}
