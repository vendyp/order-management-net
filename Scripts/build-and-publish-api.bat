rem Build the .NET application
dotnet build ..\WebApi\src\OrderManagement.WebApi\OrderManagement.WebApi.csproj

rem Build the Docker image
docker build -t ordermanagement-api:latest ..\.