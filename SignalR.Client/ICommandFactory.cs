using SignalR.Commands;

namespace SignalR.Client;

public interface ICommandFactory
{
    OperationResult<ICommand> TryCreateCommand(SignalRMessageType type, IList<string> parameters, string username);
}