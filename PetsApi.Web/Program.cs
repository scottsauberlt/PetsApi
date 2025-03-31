using Microsoft.EntityFrameworkCore;
using PetsApi.Data;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddPetsDbContext();
    builder.Services.AddControllers();

    var app = builder.Build();

    var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<PetsDbContext>();
    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    throw;
}

public partial class Program
{
}