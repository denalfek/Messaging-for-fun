FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MessagingForFun.Server.Worker/MessagingForFun.Server.Worker.csproj", "MessagingForFun.Server.Worker/"]
RUN dotnet restore "MessagingForFun.Server.Worker/MessagingForFun.Server.Worker.csproj"
COPY . .
WORKDIR "/src/MessagingForFun.Server.Worker"
RUN dotnet build "MessagingForFun.Server.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessagingForFun.Server.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessagingForFun.Server.Worker.dll"]
