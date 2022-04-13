using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class ItemListViewModel
    {
        public int Id { get; set; }

        public Category Category { get; set; }

        [StringLength(80)]
        public string Name { get; set; }

        [StringLength(250)]
        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        [StringLength(300)]
        public string Description { get; set; }
    }
}
