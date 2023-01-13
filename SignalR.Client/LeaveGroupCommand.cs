namespace SignalRClient;

public class LeaveGroupCommand : IGroupCommand
{
    public LeaveGroupCommand(string group)
    {
        Description = $"{SignalRMessageType.LeaveGroup.ToString()} <group>";
        MethodName = SignalRMessageType.LeaveGroup.ToString();
        Group = group;
    }
    
    public static int ParameterCount => 1;
    
    public string MethodName { get; init; }

    public string Description { get; init; }
    
    public string Group { get; set; }
    
    public IList<string> GetRequestParameters()
    {
        return new List<string>
        {
            Group
        };
    }
}