using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models
{
    public class Purchase
    {
        [Required]
        public int PurchaseId { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public int PurchaseQuantity { get; set; }


    }
}
