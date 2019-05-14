using Microsoft.AspNetCore.Mvc.Rendering;
using SkateShop.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkateShop.WebUI.Models
{
    public class OrderViewModel
    {
        public OrderViewModel()
        {
            CustomerList = new List<SelectListItem>();
            LocationList = new List<SelectListItem>();
            Products = new List<ProductViewModel>();
        }

        [Display(Name = "ID")]
        public int OrderId { get; set; }

        [Display(Name = "Date of Order")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Time { get; set; }

        [Display(Name = "# of Items")]
        public int Quantity { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public decimal Price { get; set; }

        public List<ProductViewModel> Products { get; set; }

        public CustomerViewModel Customer { get; set; }

        public string CustomerId { get; set; }

        public string LocationId { get; set; }
        public List<SelectListItem> CustomerList { get; set; }

        public List<SelectListItem> LocationList { get; set; }
    }
}
