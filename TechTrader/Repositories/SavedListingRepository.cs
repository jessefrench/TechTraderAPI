using Microsoft.EntityFrameworkCore;
using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Repositories
{
    public class SavedListingRepository : ISavedListingRepository
    {
        private readonly TechTraderDbContext dbContext;

        public SavedListingRepository(TechTraderDbContext context)
        {
            dbContext = context;
        }

        // get all saved listings by user id
        public async Task<List<SavedListing>> GetSavedListingsByUserIdAsync(int userId)
        {
            List<SavedListing> savedListings = await dbContext.SavedListings
                .Include(listing => listing.Listing)
                .Include(listing => listing.Listing.Seller)
                .Include(listing => listing.Listing.Category)
                .Include(listing => listing.Listing.Condition)
                .Where(savedListing => savedListing.UserId == userId).ToListAsync();

            return savedListings;
        }

        // add a saved listing for a user
        public async Task<IResult> AddSavedListingAsync(int listingId, int userId)
        {
            var listing = await dbContext.Listings.FirstOrDefaultAsync(listing => listing.Id == listingId);

            if (listing == null)
            {
                return Results.NotFound("Listing not found.");
            }

            var user = await dbContext.Users
                .Include(user => user.SavedListings)
                .FirstOrDefaultAsync(user => user.Id == userId);

            if (user == null)
            {
                return Results.NotFound("User not found.");
            }

            var isListingAlreadySaved = await dbContext.SavedListings
                .AnyAsync(savedListing => savedListing.ListingId == listingId && savedListing.UserId == userId);

            if (isListingAlreadySaved)
            {
                return Results.BadRequest("User has already saved this listing.");
            }

            var savedListing = new SavedListing
            {
                ListingId = listingId,
                UserId = userId
            };

            await dbContext.SavedListings.AddAsync(savedListing);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/saved-listings/{listingId}/add/{userId}", savedListing);
        }

        // remove a saved listing for a user
        public async Task<IResult> RemoveSavedListingAsync(int listingId, int userId)
        {
            var savedListing = await dbContext.SavedListings
                .FirstOrDefaultAsync(savedListing => savedListing.ListingId == listingId && savedListing.UserId == userId);

            if (savedListing == null)
            {
                return Results.NotFound("Saved listing not found for the specified user and listing.");
            }

            dbContext.SavedListings.Remove(savedListing);
            await dbContext.SaveChangesAsync();

            return Results.Ok($"Saved listing with ListingId {listingId} removed for UserId {userId}.");
        }
    }
}