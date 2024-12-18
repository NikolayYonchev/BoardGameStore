using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models
{
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
