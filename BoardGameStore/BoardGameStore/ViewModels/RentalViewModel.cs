using BoardGameStore.Models;
using BoardGameStore.Models.Enums;

namespace BoardGameStore.ViewModels
{
    public class RentalViewModel
    {
        public int BoardGameId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public Condition Condition { get; set; }
        public DateTime ReturnDate { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public decimal Total
        {
            get {return (RentalPricePerDay * (ReturnDate.Day - DateTime.Today.Day)); }
        }

    }
}
