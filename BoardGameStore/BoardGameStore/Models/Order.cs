using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required]
        
        public string UserId { get; set; }
        public User User { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
