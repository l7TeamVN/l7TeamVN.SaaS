using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


var deploymentPath = AppContext.BaseDirectory;
var logDirectory = Path.Combine(deploymentPath, "logs");

if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
var logFilePath = Path.Combine(logDirectory, "log-.log");
var loggerConfig = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File(
        logFilePath,
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(loggerConfig, dispose: true);

var configuration = builder.Configuration;
var services = builder.Services;

services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

services.AddControllers()
    .AddIdentityAPI();


services.AddApplicationServices();
services.AddInfrastructureServices();
services.AddPersistenceService();

services.AddIdentityModule(options =>
{
    configuration.GetSection("Modules:Identity").Bind(options);
});

services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    setupAction.AddSecurityRequirement(doc => new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", doc), new List<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseGlobalExceptionHandlerMiddleware();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
