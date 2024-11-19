using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Data;

public class TechTraderDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Condition> Conditions { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<SavedListing> SavedListings { get; set; }
    public DbSet<User> Users { get; set; }

    public TechTraderDbContext(DbContextOptions<TechTraderDbContext> context) : base(context) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(CategoryData.Categories);
        modelBuilder.Entity<Condition>().HasData(ConditionData.Conditions);
        modelBuilder.Entity<Listing>().HasData(ListingData.Listings);
        modelBuilder.Entity<Message>().HasData(MessageData.Messages);
        modelBuilder.Entity<PaymentType>().HasData(PaymentTypeData.PaymentTypes);
        modelBuilder.Entity<SavedListing>().HasData(SavedListingData.SavedListings);
        modelBuilder.Entity<User>().HasData(UserData.Users);
    }
}