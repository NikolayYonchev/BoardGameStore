using BoardGameStore.Models.Enums;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameStore.Models
{
    public class BoardGame
    {
        private int maxPlayers;
        public BoardGame()
        {
            Rentals = new HashSet<Rental>();
        }
        [Required]
        [Key]
        public int BoardGameId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public Category Category { get; set; }
        [Required]
        public Status Status { get; set; }

        [Required]
        [Display(Name ="Min Players")]
        [Range(1, 100, ErrorMessage = "Minimum players must be at least 1!")]
        public int MinPlayers { get; set; }

        [Required]
        [Display(Name = "Max Players")]
        [Range(1, 100, ErrorMessage = "Maximum players must be a valid positive number.")]
        public int MaxPlayers
        {
            get { return maxPlayers; }
            set 
            {
                if (value < MinPlayers)
                {
                    throw new ArgumentOutOfRangeException
                        ("Maximum player count should be more or equal to the minimum!");
                }
                maxPlayers = value; 
            }
        }
        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "Rental Price Per Day")]
        public decimal RentalPricePerDay { get; set; }

        [Required]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity should be more than 0")]
        public int Quantity { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }

        [Required]
        public Condition Condition { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
