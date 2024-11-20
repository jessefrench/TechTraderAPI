using TechTrader.Models;
using TechTrader.Interfaces;

namespace TechTrader.Endpoints
{
    public class MessageEndpoints
    {
        public static void Map(WebApplication app)
        {
            // get all messages
            app.MapGet("/messages", async (IMessageService messageService) =>
            {
                return await messageService.GetMessagesAsync();
            })
            .Produces<List<Message>>(StatusCodes.Status200OK);

            // get a single message by id
            app.MapGet("/messages/{messageId}", async (IMessageService messageService, int messageId) =>
            {
                Message selectedMessage = await messageService.GetMessageByIdAsync(messageId);
                return Results.Ok(selectedMessage);
            })
            .Produces<Message>(StatusCodes.Status200OK);

            // create a new message
            app.MapPost("/messages", async (IMessageService messageService, Message message) =>
            {
                var newMessage = await messageService.CreateMessageAsync(message);
                return Results.Created($"/messages/{message.Id}", message);
            })
            .Produces<Message>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // update a message
            app.MapPut("/messages/{messageId}", async (IMessageService messageService, int messageId, Message updatedMessage) =>
            {
                var messageToUpdate = await messageService.UpdateMessageAsync(messageId, updatedMessage);
                return Results.Ok(messageToUpdate);
            })
            .Produces<Message>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent);

            // delete a message
            app.MapDelete("/messages/{messageId}", async (IMessageService messageService, int messageId) =>
            {
                var messageToDelete = await messageService.DeleteMessageAsync(messageId);
                return Results.NoContent();
            })
            .Produces<Message>(StatusCodes.Status204NoContent);
        }
    }
}