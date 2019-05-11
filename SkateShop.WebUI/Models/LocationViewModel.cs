using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SkateShop.WebUI.Models
{
    public class LocationViewModel
    {
        [Display(Name = "ID")]
        public int LocationId { get; set; }

        [Required]
        public string Address { get; set; }

        //public IEnumerable<OrderViewModel> Orders { get; set; }
    }
}
