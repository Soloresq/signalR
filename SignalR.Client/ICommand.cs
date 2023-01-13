namespace SignalRClient;

public interface ICommand
{
    public string MethodName { get; init; }

    public string Description { get; init; }

    public IList<string> GetRequestParameters();
}