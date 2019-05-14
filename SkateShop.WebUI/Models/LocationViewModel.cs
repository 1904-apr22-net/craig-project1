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

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }



        public List<OrderViewModel> Orders { get; set; }
    }
}
