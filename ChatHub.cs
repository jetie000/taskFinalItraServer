using finalTaskItra.Models;
using Microsoft.AspNetCore.SignalR;

namespace finalTaskItra
{
    public sealed class ChatHub: Hub
    {
        public async Task JoinRoom( string RoomIdCon)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, RoomIdCon);
            Console.WriteLine($"User has joined {RoomIdCon}");
        }
        public async Task SendMessage(string RoomIdCon)
        {
            await Clients.Group(RoomIdCon).SendAsync("AddMessage", $"Update");
        }

    }
}
