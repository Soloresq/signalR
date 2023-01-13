namespace SignalRClient;

public interface ISignalRClient
{
    void Connect(string url);

    void Execute(ICommand signalRCommand);
}