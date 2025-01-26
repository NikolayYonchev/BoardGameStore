using BoardGameStore.Models.Enums;

namespace BoardGameStore.ViewModels
{
    public class RentalInputModel
    {
        public int BoardGameId { get; set; }
        public DateTime RentalEndDate { get; set; }

    }
}
