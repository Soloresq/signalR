# use asp.net 6.0 image in environment base
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
# set working directory inside the environment
WORKDIR /app

# use dotnet-sdk 6.0 image in new staging environment 'build'
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# set working directory inside the environment
WORKDIR /src
# copy the project file into the working directory of the environment
COPY . .
# run dotnet restore to restore nuget packages
RUN dotnet restore "SignalR.Commands/SignalR.Commands.csproj"
# run dotnet restore to restore nuget packages
RUN dotnet restore "SignalR.Client/SignalR.Client.csproj"
# copy the rest of the code for building into the working directory of the environment
COPY . .
# build application with dotnet as release into directory 'app/build' in environment 'build'
RUN dotnet build "SignalR.Client/SignalR.Client.csproj" -c Release -o /app/build

# create a new environment 'publish' by using the environment 'build' created before
FROM build AS publish
# use dotnet to publish the build artifacts into the 'app/publish' directory inside environment 'publish'
RUN dotnet publish "SignalR.Client/SignalR.Client.csproj" -c Release -o /app/publish

# create environment 'final' using environment 'base' created in first step
FROM base AS final
# set working directory inside the environment (just to be sure to be sure)
WORKDIR /app
# copy the published release build of the dotnet application from environment 'publish'
COPY --from=publish /app/publish .
# configure the entrypoint for the container
ENTRYPOINT ["dotnet", "SignalR.Client.dll"]
