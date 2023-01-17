namespace SignalR.Commands;

public class GroupMessageCommand : IMessageCommand
{
    public GroupMessageCommand(string username, string group, string message)
    {
        Description = $"{SignalRMessageType.GroupMessage.ToString()} <group> <message>";
        MethodName = SignalRMessageType.GroupMessage.ToString();
        Username = username;
        Group = group;
        Message = message;
    }

    public static int ParameterCount => 2;
    
    public string MethodName { get; init; }

    public string Description { get; init; }

    public string Group { get; init; }
    
    public string Username { get; init; }
    
    public string Message { get; init; }
}