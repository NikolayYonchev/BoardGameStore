using BoardGameStore.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGameStore.Models
{
    public class Rental
    {
        [Required]
        [Key]
        public int RentalId { get; set; }

        [Required]
        public int GameId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate
        {
            get
            {
                return ReturnDate;
            }
            set
            {
                if (ReturnDate > RentalDate)
                {
                    throw new InvalidDataException("The return date cannot be before rental date.");
                }
                ReturnDate = value;
            }
        }

        [Required]
        public Status Status { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
