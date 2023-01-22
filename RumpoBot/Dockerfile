FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RumpoBot/RumpoBot.csproj RumpoBot/
COPY ServicesLib/CommonLib.csproj ServicesLib/
COPY DiscordLib/DiscordLib.csproj DiscordLib/
RUN dotnet restore RumpoBot/RumpoBot.csproj
COPY . .
WORKDIR /src/RumpoBot
RUN dotnet build RumpoBot.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RumpoBot.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RumpoBot.dll"]
