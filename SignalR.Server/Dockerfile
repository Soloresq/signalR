# use asp.net 6.0 image in environment base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# set working directory inside the environment
WORKDIR /app
# expose the port 5001
EXPOSE 5001
# make asp.net core publish on port 5001
ENV ASPNETCORE_URLS=https://*:5001

# use latest node image in new staging environment 'build_npm'
FROM node:latest as build_npm
# set working directory inside the environment
WORKDIR /app
# Copy everything in the execution directory of the 'docker build' command into the working directory
COPY SignalR.Server/ ./
# install npm packages specified in packages.json copied in the step above
RUN npm install
# builds the package using npm packages and creates a /build directory in this staging environment 'build_npm'
RUN npm run build

# use dotnet-sdk 6.0 image in new staging environment 'build'
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# set working directory inside the environment
WORKDIR /src
# copy the project file into the working directory of the environment
COPY . .
# copy the project file into the working directory of the environment
#COPY SignalR.Commands ./
RUN apt-get update && apt-get install -y nodejs
# run dotnet restore to restore nuget packages
RUN dotnet restore "SignalR.Commands/SignalR.Commands.csproj"
# run dotnet restore to restore nuget packages
RUN dotnet restore "SignalR.Server/SignalR.Server.csproj"
# copy the rest of the code for building into the working directory of the environment
COPY . .
# build application with dotnet as release into directory 'app/build' in environment 'build'
RUN dotnet build "SignalR.Server/SignalR.Server.csproj" -c Release -o /app/build

# create a new environment 'publish' by using the environment 'build' created before
FROM build AS publish
# use dotnet to publish the build artifacts into the 'app/publish' directory inside environment 'publish'
RUN dotnet publish "SignalR.Server/SignalR.Server.csproj" -c Release -o /app/publish

# create environment 'final' using environment 'base' created in first step
FROM base AS final
# set working directory inside the environment (just to be sure to be sure)
WORKDIR /app
# copy the published release build of the dotnet application from environment 'publish'
COPY --from=publish /app/publish .
# copy the build of the node application from environment 'build_npm' into sub-directory 'build'
# make sure the '/build' directory is set as RootPath and SourcePath in the Program.cs
COPY --from=build_npm /app/wwwroot ./wwwroot
# configure the entrypoint for the container
ENTRYPOINT ["dotnet", "SignalR.Server.dll"]
