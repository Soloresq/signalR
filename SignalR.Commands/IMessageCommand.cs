namespace SignalR.Commands;

public interface IMessageCommand : ICommand
{
    public string Message { get; init; }
}