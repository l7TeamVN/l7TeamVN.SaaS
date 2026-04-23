using l7TeamVN.SaaS.Modules.Identity.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

services.AddApplicationServices();
services.AddInfrastructureServices();
services.AddPersistenceService();

services.AddIdentityModule(options =>
{
    configuration.GetSection("Modules:Identity").Bind(options);
    options.ConnectionStrings.MigrationsAssembly = Assembly.GetExecutingAssembly().GetName().Name;
});



var host = builder.Build();

using var scope = host.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();

Console.WriteLine("Running Migration ...");
await dbContext.Database.MigrateAsync();
Console.WriteLine("Database update completed!");
host.Run();
