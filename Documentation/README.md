# SignalR
Learning project for SignalR usage with ASP.NET Core

## Usecase
The Solution realizes a chat service with broadcast and group communication features.

## Code structure
The code is split into three projects

_SignalR.Server_  
Hosts the SignalR Hub and provides a web interface allowing to chat with other members 

_SignalR.Client_  
A command line interface (CLI) which provides the same functionalities as the web interface hosted by the server does

_SignalR.Commands_  
The project contains the commands used for the communication inside client and server code

## Prerequisites
- node 18.12.1 or higher
- dotnet 6.0 or higher
- Rider or VS / VS Code

## Getting started
- Clone the repository
- Check the prerequisites
- Open the IDE instances as shown in _Hint_
- In __Instance #2__ navigate to the SignalR.Server directory in a terminal and type 
    ```
    npm install
    webpack --mode=development
    ```
- Start SignalR.Server using your IDE now or type `dotnet run`
- Start SignalR.Client using your IDE now or type `dotnet run`

### Client
The CLI client requests a username first, afterwards it's going to connect to the server running at `https:localhost:5001/hub`

### Server
The server opens a web interface running at `https:localhost:5001` and starts hosting the SignalR hub at `https:localhost:5001/hub`

_Hint_  
To debug the client and server on one local machine, I opened the solution in one Rider instance __Instance #1__.
I opened only the SignalR.Server project in a second Rider instance __Instance #2__.
I opened only the SignalR.Client project in a third Rider instance __Instance #3__.
When editing code, I did this in __Instance #1__ and rebuild the whole solution and used __Instance #2__ and __Instance #3__ just for debugging.

## Docker

### Server

#### Build
- Go to the solution directory
- Type `docker build -t signalr.server.img -f SignalR.Server\Dockerfile .` to build the server image based on the dockerfile

#### Run
- Go to the solution directory
- Generate a local asp.net core ssl developer certificate using powershell  
`dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p "password"`  
`dotnet dev-certs https --trust`
- Type `docker run --name signalr.server -p 5001:5001 -e ASPNETCORE_URLS="https://*:5001" -e ASPNETCORE_HTTPS_PORT=5001 -e ASPNETCORE_Kestrel__Certificates__Default__Password="password" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -v $env:USERPROFILE\.aspnet\https:/https/ signalr.server.img` to run the docker container  
The run command specifies the environment variables making kestrel listen to the docker containers machine at port 5001 and makes the previously exported developer ssl certificate available in the docker container
- Access web app via [https://localhost:5001/](https://localhost:5001/)

### Client

#### Build
- Go to solution directory
- Type `docker build --no-cache -t signalr.client.img -f SignalR.Client\Dockerfile .`

#### Run


### References
- [Debugging ASP.NET Core in Docker](https://andrewlock.net/why-isnt-my-aspnetcore-app-in-docker-working/)
- [Running ASP.NET Core Apps with SSL in Docker](https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0#running-pre-built-container-images-with-https)
- [ASP.NET Core App configuration](https://stackoverflow.com/questions/61156388/err-empty-response-with-asp-net-core-and-docker)