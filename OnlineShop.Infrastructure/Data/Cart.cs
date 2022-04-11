/*using Microsoft.AspNetCore.Identity;
using OnlineShop.Infrastructure.Data.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure.Data
{
    public class Cart
    {
        [Key]
        [StringLength(36)]
        public int Id { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}
*/