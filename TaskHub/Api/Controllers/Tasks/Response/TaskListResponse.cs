namespace Api.Controllers.Tasks.Response;

/// <summary>
/// Ответ со списком задач
/// </summary>
public record TaskListResponse
{
    /// <summary>
    /// Список задач
    /// </summary>
    public IReadOnlyCollection<TaskResponse> Tasks { get; init; }

    /// <summary>
    /// Создает ответ со списком задач
    /// </summary>
    /// <param name="tasks">Список задач</param>
    public TaskListResponse(IReadOnlyCollection<TaskResponse> tasks)
    {
        Tasks = tasks;
    }
}