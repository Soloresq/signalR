namespace SignalRClient;

public interface IMessageCommand : ICommand
{
    public string Username { get; init; }
    
    public string Message { get; init; }
}