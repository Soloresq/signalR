using SignalR.Commands;

namespace SignalR.Client;

public interface ISignalRClient
{
    void Connect(string url);

    void Execute(ICommand signalRCommand);

    void OnBroadcastMessageReceived(string sender, string message);

    void OnGroupMessageReceived(string sender, string group, string message);

    void OnUserJoinedGroup(string sender, string group, string message);

    void OnUserLeftGroup(string sender, string group, string message);
}