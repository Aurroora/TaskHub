using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Filters;

public class RequestLoggingFilter : Attribute, IActionFilter
{
    private Stopwatch _stopwatch;

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch = Stopwatch.StartNew();

        var httpMethod = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;

        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RequestLoggingFilter>>();
        logger.LogInformation($"Начало выполнения: {httpMethod} {path}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();

        var statusCode = context.HttpContext.Response.StatusCode;
        var elapsedMs = _stopwatch.ElapsedMilliseconds;

        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<RequestLoggingFilter>>();
        logger.LogInformation($"Завершение: статус {statusCode}, время выполнения {elapsedMs} мс");
    }
}