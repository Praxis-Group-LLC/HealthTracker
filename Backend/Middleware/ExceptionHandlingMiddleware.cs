using System.Net;
using System.Text.Json;
using FluentValidation;

namespace Backend.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            // Known: FluentValidation errors → 400
            logger.LogWarning(ex, "Validation error for request {Path}", context.Request.Path);
            await WriteValidationProblemAsync(context, ex);
        }
        catch (Exception ex)
        {
            // Unknown: log + generic 500
            logger.LogError(ex, "Unhandled exception for request {Path}", context.Request.Path);
            await WriteServerErrorAsync(context);
        }
    }

    private static async Task WriteValidationProblemAsync(
        HttpContext context,
        ValidationException exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        context.Response.ContentType = "application/json";

        // Group errors by property name
        var errors = exception.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => e.ErrorMessage).ToArray()
            );

        var payload = new
        {
            type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            title = "One or more validation errors occurred.",
            status = (int)HttpStatusCode.BadRequest,
            errors
        };

        var json = JsonSerializer.Serialize(payload);
        await context.Response.WriteAsync(json);
    }

    private static async Task WriteServerErrorAsync(HttpContext context)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        var payload = new
        {
            title = "An unexpected error occurred.",
            status = (int)HttpStatusCode.InternalServerError
        };

        var json = JsonSerializer.Serialize(payload);
        await context.Response.WriteAsync(json);
    }
}
