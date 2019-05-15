using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SkateShop.WebUI.Models
{
    public class ProductViewModel
    {
        [Display(Name = "ID")]
        public int ProductId;

        public string Name;

       [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price;
    }
}
