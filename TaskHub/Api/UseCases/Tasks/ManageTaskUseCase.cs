using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Logic.Tasks.Services.Interfaces;

namespace Api.UseCases.Tasks;

/// <summary>
/// UseCase управления задачами
/// </summary>
internal sealed class ManageTaskUseCase : IManageTaskUseCase
{
    /// <summary>
    /// Сервис управления задачами
    /// </summary>
    private readonly ITaskService _taskService;

    public ManageTaskUseCase(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <inheritdoc/>
    public async Task<TaskResponse> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken)
    {
        var task = await _taskService.CreateTaskAsync(title, userId, cancellationToken);
        return new TaskResponse(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    /// <inheritdoc/>
    public async Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskService.GetAllTasksAsync(cancellationToken);

        var response = tasks
            .Select(x => new TaskResponse(x.Id, x.Title, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();

        return new TaskListResponse(response);
    }

    /// <inheritdoc/>
    public async Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId, cancellationToken);

        if (task is null)
        {
            return null;
        }

        return new TaskResponse(task.Id, task.Title, task.CreatedByUserId, task.CreatedUtc);
    }

    /// <inheritdoc/>
    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskService.DeleteAllTasksAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskService.DeleteTaskByIdAsync(taskId, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _taskService.SetTaskTitleAsync(taskId, title, cancellationToken);
    }
}