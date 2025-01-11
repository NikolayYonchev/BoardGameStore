using BoardGameStore.Models.Enums;

namespace BoardGameStore.ViewModels
{
    public class RentalInputModel
    {
        // properties needed to enter db
        //dbo - database object
        //dto - data transfer object. Used to transfer data between controller and view and reversed

        public int BoardGameId { get; set; }
        public string UserId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime ReturnDate { get; set; }
        //public Status Status { get; set; }
    }
}
