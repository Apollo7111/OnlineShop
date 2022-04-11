using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class ItemCreateViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public int CategoryId { get; set; }

    /*    [Display(Name = "Category")]
        public Category Category { get; set; }*/

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        
        [Display(Name = "Description")]
        [StringLength(300)]
        public string Description { get; set; }
    }
}
