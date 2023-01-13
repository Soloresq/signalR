namespace SignalRClient;

public interface ICommandFactory
{
    OperationResult<ICommand> TryCreateCommand(SignalRMessageType type, IList<string> parameters, string username);
}