namespace SignalR.Commands;

public interface ICommand
{
    public string MethodName { get; init; }
    
    public string Username { get; init; }

    public string Description { get; init; }
}