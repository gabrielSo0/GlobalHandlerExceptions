# Global Handler Exception with .NET 6 API

## Description
This simple .NET 6 CRUD API is to demonstrate how you can handle Global Exceptions using two diffents approaches. The ideal was to be as simple as possible, focusing on the classes and the middlewares necessary to create these global exceptions. That's why I decided to use EF Core InMemory database.

### Testing
- Install .NET 6 SDK. If you already have, you're good to go.
- Clone the repo on your machine.
- VSCode environment: inside the project's folder, open a terminal, or your VScode. Execute the command 'dotnet build', and 'dotnet run' to run the API.
- VS: Just execute the project within the Visual Studio.
- To send request, use postman or similar (basic url: https:localhost:port/user)
- To see how it would handle the global exception, just add an 'throw new Exception("Error")' within the endpoint you're testing.
