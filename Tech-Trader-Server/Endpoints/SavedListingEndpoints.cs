using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class SavedListingEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get saved listings by user id
            app.MapGet("/saved-listings/{userId}", async (ISavedListingService savedListingService, int userId) =>
            {
                return await savedListingService.GetSavedListingsByUserIdAsync(userId);
            })
            .Produces<List<SavedListing>>(StatusCodes.Status200OK);

            // add a saved listing for a user
            app.MapPost("/saved-listings/{listingId}/add/{userId}", async (ISavedListingService savedListingService, int listingId, int userId) =>
            {
                var result = await savedListingService.AddSavedListingAsync(listingId, userId);
                return result;
            })
            .Produces<IResult>(StatusCodes.Status204NoContent);

            // remove a saved listing from a user
            app.MapDelete("/saved-listings/{listingId}/remove/{userId}", async (ISavedListingService savedListingService, int listingId, int userId) =>
            {
                var result = await savedListingService.RemoveSavedListingAsync(listingId, userId);
                return result;
            })
            .Produces<IResult>(StatusCodes.Status204NoContent);
        }
    }
}