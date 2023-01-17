namespace SignalR.Commands;

public interface IGroupCommand : ICommand
{
    public string Group { get; set; }
}