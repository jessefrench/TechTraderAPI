using TechTrader.Models;

namespace TechTrader.Data
{
    public class UserData
    {
        public static List<User> Users = new()
        {
            new User
            {
                Id = 1,
                Uid = "PzXnYW3LbJkfVT92QLrCoM87F15N4",
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@example.com",
                ImageUrl = "https://example.com/images/alice.jpg",
                City = "San Francisco",
                State = "CA",
                Zip = "94103",
                IsSeller = true
            },
            new User
            {
                Id = 2,
                Uid = "ZpkMvJ2YWbxELQ39VTfXrK8C76M4",
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@example.com",
                ImageUrl = "https://example.com/images/bob.jpg",
                City = "Austin",
                State = "TX",
                Zip = "73301",
                IsSeller = true
            },
            new User
            {
                Id = 3,
                Uid = "QlmPnKY2WvcrLJ37TZfXoC8F65N9",
                FirstName = "Cathy",
                LastName = "Lee",
                Email = "cathy.lee@example.com",
                ImageUrl = "https://example.com/images/cathy.jpg",
                City = "Seattle",
                State = "WA",
                Zip = "98101",
                IsSeller = true
            }
        };
    }
}