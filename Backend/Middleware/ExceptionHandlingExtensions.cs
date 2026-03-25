namespace Backend.Middleware;

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseHealthTrackerExceptionHandling(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}