namespace SignalR.Client;

public class OperationResult<T> where T : class
{
    public OperationResult(string errorMessage = "")
    {
        Success = false;
        ErrorMessage = errorMessage;
    }

    public OperationResult(T? result = null)
    {
        Success = true;
        Result = result;
    }

    public string? ErrorMessage { get; init; }

    public bool Success { get; init; }

    public T? Result { get; init; }
}