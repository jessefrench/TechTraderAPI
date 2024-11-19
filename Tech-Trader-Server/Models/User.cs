namespace TechTrader.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl {  get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip {  get; set; }
        public bool IsSeller { get; set; }
    }
}