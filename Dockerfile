# Dockerfile para aplicação ASP.NET Core (.NET 8)
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["NexoWebApplication/NexoWebApplication.csproj", "NexoWebApplication/"]
COPY ["NexoWebApplication.Domain/NexoWebApplication.Domain.csproj", "NexoWebApplication.Domain/"]
COPY ["NexoWebApplication.Application/NexoWebApplication.Application.csproj", "NexoWebApplication.Application/"]
COPY ["NexoWebApplication.Infrastructure/NexoWebApplication.Infrastructure.csproj", "NexoWebApplication.Infrastructure/"]
COPY . .
RUN dotnet restore "NexoWebApplication/NexoWebApplication.csproj"
RUN dotnet publish "NexoWebApplication/NexoWebApplication.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:5233
EXPOSE 5233
ENTRYPOINT ["dotnet", "NexoWebApplication.dll"]
