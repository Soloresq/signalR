using Microsoft.AspNetCore.SignalR;
using SignalR.Commands;

namespace SignalRWebpack.Hubs;

public class ChatHub : Hub
{
    public async Task BroadcastMessage(BroadcastMessageCommand command)
    {
        // await Clients.AllExcept(Context.ConnectionId).SendAsync(command.MethodName, command); // Do not call the MessageReceived action at the sender of the message
        await Clients.All.SendAsync(command.MethodName, command); // Calls the MessageReceived action at all clients
    }
    
    public async Task GroupMessage(GroupMessageCommand command)
    {
        await Clients.Group(command.Group).SendAsync(command.MethodName, command);
    }

    public async Task JoinGroup(JoinGroupCommand command)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, command.Group);
        await GroupMessage(new GroupMessageCommand(command.Username, command.Group, "User joined group"));
    }
    
    public async Task LeaveGroup(LeaveGroupCommand command)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, command.Group);
        await GroupMessage(new GroupMessageCommand(command.Username, command.Group, "User left group"));
    }
}
