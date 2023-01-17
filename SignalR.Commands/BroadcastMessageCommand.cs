namespace SignalR.Commands;

public class BroadcastMessageCommand : IMessageCommand
{
    public BroadcastMessageCommand(string username, string message)
    {
        Description = $"{SignalRMessageType.BroadcastMessage.ToString()} <message>";
        MethodName = SignalRMessageType.BroadcastMessage.ToString();
        Username = username;
        Message = message;
    }
    
    public string MethodName { get; init; }

    public string Description { get; init; }

    public string Username { get; init; }
    
    public string Message { get; init; }

    public static int ParameterCount => 1;
}