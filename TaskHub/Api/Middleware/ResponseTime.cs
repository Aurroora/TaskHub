using System.Diagnostics;

namespace Api.Middleware;

public class ResponseTime
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ResponseTime> _logger;

    public ResponseTime(RequestDelegate next, ILogger<ResponseTime> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        context.Response.OnStarting(() =>
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            context.Response.Headers.Append("X-Response-Time-Ms", elapsedMs.ToString());
            return Task.CompletedTask;
        });
        
        await _next(context);
    }
}