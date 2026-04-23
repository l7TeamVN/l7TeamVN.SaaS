using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace l7TeamVN.SaaS.Infrastructure.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            object response;

            response = new ProblemDetails
            {
                Title = "Server Error",
                Detail = ex.Message,
                Status = context.Response.StatusCode,
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
