using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Api.Attributes;

public class ValidateUserRequestAttribute : Attribute, IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Count == 0)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        var requestBody = context.ActionArguments.First().Value;
        if (requestBody == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        var nameProperty = requestBody.GetType().GetProperty("Name",
            BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        if (nameProperty == null)
        {
            return;
        }

        var nameValue = nameProperty.GetValue(requestBody) as string;

        if (string.IsNullOrWhiteSpace(nameValue))
        {
            context.Result = new BadRequestObjectResult("Имя пользователя не задано");
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}