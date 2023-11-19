using finalTaskItra.Models;
using Microsoft.AspNetCore.SignalR;

namespace finalTaskItra
{
    public sealed class ChatHub: Hub
    {
        public async Task JoinRoom( UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomIdCon);
            Console.WriteLine($"{userConnection.UserName} has joined {userConnection.RoomIdCon}");
            await Clients.Group(userConnection.RoomIdCon).SendAsync("JoinMessage", $"{userConnection.UserName} has joined {userConnection.RoomIdCon}");
        }

        public async Task LeaveRoom(UserConnection userConnection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.RoomIdCon);
            Console.WriteLine($"{userConnection.UserName} has left {userConnection.RoomIdCon}");
            await Clients.Group(userConnection.RoomIdCon).SendAsync("LeaveMessage", userConnection.UserName);
        }
        public async Task SendUsername(UserConnection userConnection)
        {
            Console.WriteLine(userConnection.RoomIdCon + "    " + userConnection.UserName+ "  sended username");
            await Clients.Group(userConnection.RoomIdCon).SendAsync("ReceiveUsernameMessage", userConnection.UserName);
        }

        public async Task SendMessage(UserConnectionMessage userConnectionMessage)
        {
            await Clients.Group(userConnectionMessage.RoomIdCon).SendAsync("ReceiveMessage", userConnectionMessage.UserName);
            Console.WriteLine(userConnectionMessage.RoomIdCon + "    " + userConnectionMessage.UserName + "  " + userConnectionMessage.Message);
        }

        public async Task CancelDrawing(UserConnectionMessage userConnectionMessage)
        {
            await Clients.Group(userConnectionMessage.RoomIdCon).SendAsync("CancelDrawingMessage", userConnectionMessage.UserName);
            Console.WriteLine(userConnectionMessage.RoomIdCon + "    " + userConnectionMessage.UserName);
        }
    }
}
