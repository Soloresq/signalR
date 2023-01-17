namespace SignalR.Commands;

public class JoinGroupCommand : IGroupCommand
{
    public JoinGroupCommand(string group, string username)
    {
        Description = $"{SignalRMessageType.JoinGroup.ToString()} <group>";
        MethodName = SignalRMessageType.JoinGroup.ToString();
        Username = username;
        Group = group;
    }
    
    public static int ParameterCount => 1;
    
    public string MethodName { get; init; }
    
    public string Username { get; init; }

    public string Description { get; init; }
    
    public string Group { get; set; }
}