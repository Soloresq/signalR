﻿// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SignalRClient;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<ICommandLineInterpreter, CommandLineInterpreter>();
        services.AddSingleton<ICommandFactory, CommandFactory>();
        services.AddSingleton<ISignalRClient, SignalRClient.SignalRClient>();
    })
    .Build();

ISignalRClient signalRClient = host.Services.GetRequiredService<ISignalRClient>();
signalRClient.Connect("https://localhost:5001/hub");

ICommandLineInterpreter commandLineInterpreter = host.Services.GetRequiredService<ICommandLineInterpreter>();
commandLineInterpreter.Run();
