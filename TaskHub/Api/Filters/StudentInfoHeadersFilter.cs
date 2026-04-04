using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class StudentInfoHeadersFilter : Attribute, IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.TryAdd("X-Student-Name", "Popova Ann Mikhailovna");
        context.HttpContext.Response.Headers.TryAdd("X-Student-Group", "RI-240932");
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}