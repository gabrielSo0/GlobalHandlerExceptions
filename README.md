# Global Handler Exception with .NET 6 API

## Description
A simple .NET 6 CRUD API to demonstrate how you can handle Global Exceptions using two diffents approaches. The ideal was to be as simple as possible, focusing on the classes and the middlewares necessary to create these global exceptions. That's why I decided to use EF Core InMemory database.

### Testing
- Install .NET 6 SDK. If you already have, you're good to go.
- Clone the repo on your machine.
- VSCode environment: inside the project's folder, open a terminal, or your VScode. Execute the command 'dotnet build', and 'dotnet run' to run the API.
- VS environment: Just execute the project within the Visual Studio.
- To send request, use postman or similar (basic url: https:localhost:port/user)
- To see how it would handle the global exception, just add an 'throw new Exception("Error")' within the endpoint you're testing.

### Global Exception, first approach using built-in middleware and methods
We need to create a class, an extension method for the IApplicationBuilder interface and a response class 'ErrorDetails'. In my case I called it 'ConfigureExceptionHandler'. Inside the method there is the implementation of the UseExceptionHandler that it is going to intercept the requests and if there is an unhandler error, it will return a pattern status code and message as a response.

Now, you need to register the middleware on the Program class 'app.ConfigureExceptionHandler();' and there you go. There is a global handler exception on your API.

### Custom Global Exception as the second approach
If you want to make the message, custom status code and others things, you can create your own custom global exception middleware.

Create a class "GlobalExceptionMiddleware" or something like that, inject the RequestDelegate class and create a InvokeAsync method to intercept the requests and deal with and unhandle error. You can send custom status code, title and details depending of the exception you receive, the class ProblemDetails can be used easily to create the object response. The HandleExceptionAsync method is going to serialize all this and return to the client as a ProblemDetails object.

Now, you need to create an extension method for the 'WebApplication' class, 'ConfigureCustomExceptionHandler' for example, and use the method 'app.UseMiddleware<GlobalExceptionMiddleware>();' passing the class you've previous created.
Inside the Program class, need to register this middleware.
  
## NOTE
There is something to note, your global exception will only be the response if the error that occured was not handled by a try/catch. So, the try/catch block will always take precedence.
