using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class SavedListingEndpoints
    {
        public static void MapSavedListingEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/saved-listings").WithTags(nameof(SavedListing));

            // get saved listings by user id
            group.MapGet("/{userId}", async (ISavedListingService savedListingService, int userId) =>
            {
                return await savedListingService.GetSavedListingsByUserIdAsync(userId);
            })
            .WithName("GetSavedListingsByUserId")
            .WithOpenApi()
            .Produces<List<SavedListing>>(StatusCodes.Status200OK);

            // add a saved listing for a user
            group.MapPost("/{listingId}/add/{userId}", async (ISavedListingService savedListingService, int listingId, int userId) =>
            {
                var result = await savedListingService.AddSavedListingAsync(listingId, userId);
                return result;
            })
            .WithName("AddSavedListing")
            .WithOpenApi()
            .Produces<IResult>(StatusCodes.Status204NoContent);

            // remove a saved listing from a user
            group.MapDelete("/{listingId}/remove/{userId}", async (ISavedListingService savedListingService, int listingId, int userId) =>
            {
                var result = await savedListingService.RemoveSavedListingAsync(listingId, userId);
                return result;
            })
            .WithName("RemoveSavedListing")
            .WithOpenApi()
            .Produces<IResult>(StatusCodes.Status204NoContent);
        }
    }
}