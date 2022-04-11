using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class CategoryListViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Name")]
        public string Name { get; set; }
    }
}
