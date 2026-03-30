using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Attributes;

public class FromRouteTaskIdAttribute : ModelBinderAttribute
{
    public FromRouteTaskIdAttribute() : base(typeof(TaskIdModelBinder))
    {
        BindingSource = BindingSource.Path;
        Name = "id";
    }
}

public class TaskIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue("id");

        if (valueProviderResult == ValueProviderResult.None)
        {
            bindingContext.ModelState.AddModelError("id", "Идентификатор задачи не задан");
            return Task.CompletedTask;
        }

        var idValue = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(idValue))
        {
            bindingContext.ModelState.AddModelError("id", "Идентификатор задачи не задан");
            return Task.CompletedTask;
        }

        if (!Guid.TryParse(idValue, out var guidValue))
        {
            bindingContext.ModelState.AddModelError("id", "Идентификатор задачи имеет некорректный формат");
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(guidValue);
        return Task.CompletedTask;
    }
}