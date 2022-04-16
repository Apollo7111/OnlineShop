using OnlineShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
    public class OrderCreateViewModel
    {
        public DateTime Date { get; set; }

        public int UserId { get; set; }

        [StringLength(25)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [StringLength(25)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "PhoneNumber")]
        [StringLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(70)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(200)]
        [Display(Name = "AdditionalInformation")]
        public string AdditionalInformation { get; set; }

        public IList<Item> Cart { get; set; } = new List<Item>();
    }
}
