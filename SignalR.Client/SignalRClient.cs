using System.Diagnostics;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient;

public class SignalRClient : ISignalRClient
{
    private HubConnection? connection;
    
    public void Execute(ICommand signalRCommand)
    {
        if (connection == null || connection.State != HubConnectionState.Connected)
        {
            throw new InvalidOperationException("The connection was not yet established.");
        }

        var parameters = signalRCommand.GetRequestParameters();
        connection.InvokeAsync(signalRCommand.MethodName, parameters);
    }
    
    public async void Connect(string url)
    {
        connection = new HubConnectionBuilder()
        .WithUrl(url)
        .WithAutomaticReconnect()
        .Build();

        connection.Reconnecting += error =>
        {
            Debug.Assert(connection.State == HubConnectionState.Reconnecting);
            // Notify users the connection was lost and the client is reconnecting.
            // Start queuing or dropping messages.
            Debug.WriteLine(error?.Message);
            return Task.CompletedTask;
        };

        connection.Reconnected += _ =>
        {
            Debug.Assert(connection.State == HubConnectionState.Connected);
            // Notify users the connection was reestablished.
            // Start dequeuing messages queued while reconnecting if any.
            return Task.CompletedTask;
        };

        connection.Closed += _ =>
        {
            Debug.Assert(connection.State == HubConnectionState.Disconnected);
            // Notify users the connection has been closed or manually try to restart the connection.
            return Task.CompletedTask;
        };

        await ConnectWithRetryAsync(connection, new CancellationToken());
    }

    static async Task<bool> ConnectWithRetryAsync(HubConnection connection, CancellationToken token)
    {
        // Keep trying to until we can start or the token is canceled.
        while (true)
        {
            try
            {
                await connection.StartAsync(token);
                Debug.Assert(connection.State == HubConnectionState.Connected);
                Console.WriteLine($"Established connection {connection.ConnectionId}");
                return true;
            }
            catch when (token.IsCancellationRequested)
            {
                return false;
            }
            catch
            {
                // Failed to connect, trying again in 5000 ms.
                Debug.Assert(connection.State == HubConnectionState.Disconnected);
                await Task.Delay(5000, token);
            }
        }
    }
}