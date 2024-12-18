using BoardGameStore.Models.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;

namespace BoardGameStore.Models
{
    public class Game
    {
        [Required]
        public int GameId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }

        [Required]
        public int MinPlayers { get; set; }

        [Required]
        public int MaxPlayers { get; set; }

        public string? Description { get; set; }

        [Required]
        public decimal RentalPricePerDay { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public Condition Condition { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
