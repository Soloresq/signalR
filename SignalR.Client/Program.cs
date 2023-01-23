﻿// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using SignalRClient;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICommandLineInterpreter, CommandLineInterpreter>();
        services.AddSingleton<ICommandFactory, CommandFactory>();
        services.AddSingleton<ISignalRClient, SignalRClient.SignalRClient>();
    })
    .Build();

IConfiguration Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
				
var signalRHub = Config.GetSection("SignalRHub").Value;

ISignalRClient signalRClient = host.Services.GetRequiredService<ISignalRClient>();
signalRClient.Connect($"https://{signalRHub}:5001/hub");

Console.WriteLine(signalRHub);
ICommandLineInterpreter commandLineInterpreter = host.Services.GetRequiredService<ICommandLineInterpreter>();
commandLineInterpreter.Run();
