using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ValidateSetTaskTitleRequestFilter : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // есть ли тела запроса
        if (!context.ActionArguments.TryGetValue("request", out var requestValue) || requestValue == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}