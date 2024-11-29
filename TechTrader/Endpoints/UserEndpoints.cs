using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/users").WithTags(nameof(User));

            // get a single user by id
            group.MapGet("/{userId}", async (IUserService userService, int userId) =>
            {
                User selectedUser = await userService.GetUserByIdAsync(userId);
                return Results.Ok(selectedUser);
            })
            .WithName("GetUserById")
            .WithOpenApi()
            .Produces<User>(StatusCodes.Status200OK);

            // create a new user
            group.MapPost("/", async (IUserService userService, User user) =>
            {
                var newUser = await userService.CreateUserAsync(user);
                return Results.Created($"/users/{user.Id}", user);
            })
            .WithName("CreateUser")
            .WithOpenApi()
            .Produces<User>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a user
            group.MapPut("/{userId}", async (IUserService userService, int userId, User updatedUser) =>
            {
                var userToUpdate = await userService.UpdateUserAsync(userId, updatedUser);
                return Results.Ok(userToUpdate);
            })
            .WithName("UpdateUser")
            .WithOpenApi()
            .Produces<PaymentType>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);
        }
    }
}