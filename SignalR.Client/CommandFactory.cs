namespace SignalRClient;

public class CommandFactory : ICommandFactory
{
    public OperationResult<ICommand> TryCreateCommand(SignalRMessageType type, IList<string> parameters, string username)
    {
        switch (type)
        {
            case SignalRMessageType.BroadcastMessage:
                return TryCreateBroadcastMessageCommand(parameters, username);
            case SignalRMessageType.GroupMessage:
                return TryCreateGroupMessageCommand(parameters, username);
            case SignalRMessageType.JoinGroup:
                return TryCreateJoinGroupCommand(parameters);
            case SignalRMessageType.LeaveGroup: 
                return TryCreateLeaveGroupCommand(parameters);
            default:
                throw new NotImplementedException();
        }
    }

    private OperationResult<ICommand> TryCreateBroadcastMessageCommand(IList<string> parameters, string username)
    {
        if (parameters.Count != BroadcastMessageCommand.ParameterCount)
        {
            return new OperationResult<ICommand>("Invalid number of parameters.");
        }
        
        return new OperationResult<ICommand>(new BroadcastMessageCommand(username, parameters[0]));
    }
    
    private OperationResult<ICommand> TryCreateGroupMessageCommand(IList<string> parameters, string username)
    {
        if (parameters.Count != GroupMessageCommand.ParameterCount)
        {
            return new OperationResult<ICommand>("Invalid number of parameters.");
        }
        
        return new OperationResult<ICommand>(new GroupMessageCommand(username, parameters[0], parameters[1]));
    }
    
    private OperationResult<ICommand> TryCreateJoinGroupCommand(IList<string> parameters)
    {
        if (parameters.Count != JoinGroupCommand.ParameterCount)
        {
            return new OperationResult<ICommand>("Invalid number of parameters.");
        }

        return new OperationResult<ICommand>(new JoinGroupCommand(parameters[0]));
    }
    
    private OperationResult<ICommand> TryCreateLeaveGroupCommand(IList<string> parameters)
    {
        if (parameters.Count != LeaveGroupCommand.ParameterCount)
        {
            return new OperationResult<ICommand>("Invalid number of parameters.");
        }

        return new OperationResult<ICommand>(new LeaveGroupCommand(parameters[0]));
    }
}