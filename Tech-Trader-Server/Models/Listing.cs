namespace TechTrader.Models
{
    public class Listing
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public int ConditionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Sold { get; set; }
        public User Seller { get; set; }
        public Category Category { get; set; }
        public Condition Condition { get; set; }
    }
}