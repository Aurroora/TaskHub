using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class StudentInfoHeadersAttribute : ResultFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.TryAdd(
            "X-Student-Name",
            "Popova Ann Mikhailovna"
        );

        context.HttpContext.Response.Headers.TryAdd(
            "X-Student-Group",
            "RI-240932"
        );

        base.OnResultExecuting(context);
    }
}