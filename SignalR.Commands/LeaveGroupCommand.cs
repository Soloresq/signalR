namespace SignalR.Commands;

public class LeaveGroupCommand : IGroupCommand
{
    public LeaveGroupCommand(string group, string username)
    {
        Description = $"{SignalRMessageType.LeaveGroup.ToString()} <group>";
        MethodName = SignalRMessageType.LeaveGroup.ToString();
        Username = username;
        Group = group;
    }
    
    public static int ParameterCount => 1;
    
    public string MethodName { get; init; }
    
    public string Username { get; init; }

    public string Description { get; init; }
    
    public string Group { get; set; }
}