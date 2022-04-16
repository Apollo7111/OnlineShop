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
    public class Order
    {
        [Key]
        [StringLength(36)]
        public int Id { get; set; }
       public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(200)]
        public string AdditionalInformation { get; set; }

        public IList<Item> Cart { get; set; }

    }
}
