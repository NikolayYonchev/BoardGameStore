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

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
       
    }
}
