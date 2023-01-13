namespace SignalRClient;

public class JoinGroupCommand : IGroupCommand
{
    public JoinGroupCommand(string group)
    {
        Description = $"{SignalRMessageType.JoinGroup.ToString()} <group>";
        MethodName = SignalRMessageType.JoinGroup.ToString();
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