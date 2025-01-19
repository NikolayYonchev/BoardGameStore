namespace BoardGameStore.ViewModels
{
	public class RentalOutputModel
	{
        public int BoardGameId { get; set; }
        public decimal Total { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
