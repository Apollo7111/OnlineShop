using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Data
{
    public class Category
    {
        [Key]
        [StringLength(36)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }
       public List<Item> Items { get; set; } = new List<Item>();
    }
}
