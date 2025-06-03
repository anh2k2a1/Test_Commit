using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace API.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestPath = context.Request.Path;
        var method = context.Request.Method;

        try
        {
            _logger.LogInformation("Request started: {Method} {Path}", method, requestPath);
            await _next(context);
            stopwatch.Stop();
            
            _logger.LogInformation("Request completed: {Method} {Path} - Status: {StatusCode} - Duration: {Duration}ms", 
                method, requestPath, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();
            _logger.LogError(ex, "Request failed: {Method} {Path} - Duration: {Duration}ms", 
                method, requestPath, stopwatch.ElapsedMilliseconds);
            throw;
        }
    }
}