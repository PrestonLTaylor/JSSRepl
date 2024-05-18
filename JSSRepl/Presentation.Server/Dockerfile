FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80 
EXPOSE 443 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JSSRepl/Presentation.Server/Presentation.Server.csproj", "JSSRepl/Presentation.Server/"]
COPY ["JSSRepl/Application/Application.csproj", "JSSRepl/Application/"]
COPY ["JSSRepl/Domain/Domain.csproj", "JSSRepl/Domain/"]
COPY ["JSSRepl/Presentation.Client/Presentation.Client.csproj", "JSSRepl/Presentation.Client/"]
RUN dotnet restore "./JSSRepl/Presentation.Server/Presentation.Server.csproj"
COPY . .
WORKDIR "/src/JSSRepl/Presentation.Server"
RUN dotnet build "./Presentation.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Presentation.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.Server.dll"]