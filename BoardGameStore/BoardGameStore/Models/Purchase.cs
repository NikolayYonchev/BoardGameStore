using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameStore.Models
{
    public class Purchase
    {
        [Required]
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("BoardGame")]
        public int BoardGameId { get; set; }

        [Required]
        public DateTime PurchaseDate { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Purchase quantity must be at least 1.")]
        public int PurchaseQuantity { get; set; }

        public decimal Total { get; set; }

        public User User { get; set; }

        public BoardGame BoardGame { get; set; }

    }
}
