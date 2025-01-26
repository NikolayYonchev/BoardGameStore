using BoardGameStore.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameStore.Models
{
    public class Rental
    {
        private DateTime returnDate;

        [Required]
        [Key]
        public int RentalId { get; set; }

        [Required]
        [ForeignKey("BoardGame")]
        public int BoardGameId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }
        
		public DateTime ReturnDate
		{
			get
			{
				return returnDate;
			}
			set
			{
				if (value < RentalDate)
				{
					throw new InvalidDataException("The return date cannot be before rental date.");
				}
				returnDate = value;
			}
		}
        [Required]
        public decimal Total { get; set; }

        public BoardGame BoardGame { get; set; }


        public User User { get; set; }
    }
}
