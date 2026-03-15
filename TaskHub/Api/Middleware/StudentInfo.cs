namespace Api.Middleware;

public class StudentInfo
{
    private readonly RequestDelegate _next;

    public StudentInfo(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.OnStarting(() =>
        {
            context.Response.Headers.Append("X-Student-Name", "Popova Anna Mikhailovna");
            context.Response.Headers.Append("X-Student-Group", "RI-240932");
            return Task.CompletedTask;
        });

        await _next(context);
    }
}