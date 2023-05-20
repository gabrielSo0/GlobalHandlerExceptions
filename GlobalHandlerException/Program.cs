using GlobalHandlerException.Data;
using GlobalHandlerException.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(op =>
{
    op.UseInMemoryDatabase("UsersDb");
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.ConfigureCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
