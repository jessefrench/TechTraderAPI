namespace TechTrader.Models
{
    public class SavedListing
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ListingId { get; set; }
        public Listing Listing { get; set; }
    }
}