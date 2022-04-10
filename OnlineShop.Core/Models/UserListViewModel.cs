using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class UserListViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public ICollection<Item> Cart { get; set; }
    }
}
