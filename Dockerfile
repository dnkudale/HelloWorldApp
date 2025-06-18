# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
 
COPY . ./
RUN dotnet restore HelloWorldApp.csproj
RUN dotnet publish HelloWorldApp.csproj -c Release -o /app
 
# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
 
ENTRYPOINT ["dotnet", "HelloWorldApp.dll"]
