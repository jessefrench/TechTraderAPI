using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class UserEndpoints
    {
        public static async void Map(WebApplication app)
        {
            // get a single user by id
            app.MapGet("/users/{userId}", async (IUserService userService, int userId) =>
            {
                User selectedUser = await userService.GetUserByIdAsync(userId);
                return Results.Ok(selectedUser);
            })
            .Produces<User>(StatusCodes.Status200OK);

            // create a new user
            app.MapPost("/users", async (IUserService userService, User user) =>
            {
                var newUser = await userService.CreateUserAsync(user);
                return Results.Created($"/users/{user.Id}", user);
            })
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a user
            app.MapPut("/users/{userId}", async (IUserService userService, int userId, User updatedUser) =>
            {
                var userToUpdate = await userService.UpdateUserAsync(userId, updatedUser);
                return Results.Ok(userToUpdate);
            })
            .Produces<PaymentType>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);
        }
    }
}