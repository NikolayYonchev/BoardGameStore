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
        [ForeignKey("BoardGame")]
        public int BoardGameId { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }


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

        public BoardGame BoardGame { get; set; }

        public Order Order { get; set; }

        public User User { get; set; }
    }
}
