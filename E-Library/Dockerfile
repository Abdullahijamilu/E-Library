# Use .NET 9.0 SDK for build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy only the project file to restore dependencies
COPY ["E-Library.csproj", "./"]

RUN dotnet restore "E-Library.csproj"

# Copy the rest of the application code
COPY . .

# Build the project
RUN dotnet build "E-Library.csproj" -c Release -o /app/build

# Publish the project
FROM build AS publish
RUN dotnet publish "E-Library.csproj" -c Release -o /app/publish

# Use .NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Library.dll"]
