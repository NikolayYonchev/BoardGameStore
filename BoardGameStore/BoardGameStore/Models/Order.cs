namespace BoardGameStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<Purchase> Purchases { get; set; }

    }
}
