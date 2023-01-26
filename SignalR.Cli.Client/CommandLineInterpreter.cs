using System.Text.RegularExpressions;
using SignalR.Commands;

namespace SignalR.Client;

public class CommandLineInterpreter : ICommandLineInterpreter
{
    private readonly ICommandFactory factory;
    private readonly ISignalRClient signalRClient;
    private readonly Regex commandRegex = new("[^\\s\"']+|\"([^\"]*)\"|'([^']*)'");
    private string? username;

    public CommandLineInterpreter(ICommandFactory factory, ISignalRClient signalRClient)
    {
        this.factory = factory;
        this.signalRClient = signalRClient;
    }
    
    public void Run()
    {
        bool listen = true;

        while (username == null)
        {
            Console.WriteLine("Please enter your username");
            username = Console.ReadLine();
        }

        Console.WriteLine("Type 'JoinGroup <groupName>' to enter a group.");
        Console.WriteLine("Type 'LeaveGroup <groupName>' to remove a group.");
        Console.WriteLine("Type 'BroadcastMessage <message>' to send a message to every member");
        Console.WriteLine("Type 'GroupMessage <groupName> <message>' to send a message to every member of the specified group");
        Console.WriteLine("Type 'Quit' or 'Exit' to close the connection");
        
        while (listen)
        {
            string? input = Console.ReadLine();
            listen = Interpret(input);
        }
    }

    private bool Interpret(string? commandString)
    {
        if (string.IsNullOrEmpty(commandString))
        {
            return true;
        }
        
        var commandChunks = commandRegex.Matches(commandString);

        if (commandChunks.Count == 0)
        {
            Console.WriteLine("Invalid input");
            return true;
        }

        if (commandChunks[0].Value.ToLower().Equals("quit") || commandChunks[0].Value.ToLower().Equals("exit"))
        {
            return false;
        }
        
        if (Enum.TryParse(commandChunks[0].Value, true, out SignalRMessageType messageType))
        {
            var parameters = commandChunks.Select(x => x.Value).ToList();
            parameters.RemoveAt(0);
            
            var result = factory.TryCreateCommand(messageType, parameters, username);
            if (result.Success)
            {
                signalRClient.Execute(result.Result!);
                return true;
            }

            Console.WriteLine(result.ErrorMessage);
            return true;
        }
        
        Console.WriteLine($"Command '{commandChunks[0].Value}' does not exist");
        return true;
    }
}