using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class ResponseTimeHeaderAttribute : ActionFilterAttribute
{
    private readonly Stopwatch _stopwatch = new Stopwatch();

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _stopwatch.Start();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _stopwatch.Stop();
        context.HttpContext.Response.Headers.TryAdd(
            "X-Response-Time-Ms",
            _stopwatch.ElapsedMilliseconds.ToString()
        );
    }
}
