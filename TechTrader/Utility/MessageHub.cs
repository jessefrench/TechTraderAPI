using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace TechTrader.Utility
{
    public class MessageHub : Hub
    {
        private static readonly ConcurrentDictionary<int, string> UserConnections = new();

        public async Task RegisterUser(int userId)
        {
            if (userId != 0)
            {
                UserConnections[userId] = Context.ConnectionId;
                Console.WriteLine($"User {userId} connected with Connection ID: {Context.ConnectionId}");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = UserConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;
            if (userId != 0)
            {
                UserConnections.TryRemove(userId, out _);
                Console.WriteLine($"User {userId} disconnected.");
            }
            await base.OnDisconnectedAsync(exception);
        }

        public static string? GetConnectionId(int userId)
        {
            return UserConnections.TryGetValue(userId, out var connectionId) ? connectionId : null;
        }
    }
}