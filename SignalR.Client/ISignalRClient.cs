using SignalR.Commands;

namespace SignalR.Client;

public interface ISignalRClient
{
    void Connect(string url);

    void Execute(ICommand signalRCommand);
}