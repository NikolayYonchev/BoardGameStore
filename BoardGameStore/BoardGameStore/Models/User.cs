using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models
{
    public class User
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
            Purchases = new HashSet<Purchase>();
        }

        [Required]
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string PhoneNumber { get; set; }

        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
