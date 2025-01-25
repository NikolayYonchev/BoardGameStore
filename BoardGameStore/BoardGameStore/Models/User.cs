using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            Rentals = new HashSet<Rental>();
            Purchases = new HashSet<Purchase>();
        }

        //[Required]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]

        //public string PhoneNumber { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
       
    }
}
