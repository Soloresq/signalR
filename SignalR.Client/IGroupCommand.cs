namespace SignalRClient;

public interface IGroupCommand : ICommand
{
    public string Group { get; set; }
}