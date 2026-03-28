using Dal.Repositories;
using Dal.Repositories.Interfaces;
using Logic.Tasks.Models;
using Logic.Tasks.Services.Interfaces;

namespace Logic.Tasks.Services;

/// <inheritdoc />
public sealed class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    /// <inheritdoc />
    public async Task<TaskModel> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.CreateAsync(title, userId, cancellationToken);
        return new TaskModel(task.Id, task.Title ?? string.Empty, task.CreatedByUserId, task.CreatedUtc);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TaskModel>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAllAsync(cancellationToken);

        var result = tasks
            .Select(x => new TaskModel(x.Id, x.Title ?? string.Empty, x.CreatedByUserId, x.CreatedUtc))
            .ToList()
            .AsReadOnly();

        return result;
    }

    /// <inheritdoc />
    public async Task<TaskModel?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(taskId, cancellationToken);

        if (task == null)
        {
            return null;
        }

        return new TaskModel(task.Id, task.Title ?? string.Empty, task.CreatedByUserId, task.CreatedUtc);
    }

    /// <inheritdoc />
    public async Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken)
    {
        await _taskRepository.UpdateTitleAsync(taskId, title, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken)
    {
        return await _taskRepository.DeleteByIdAsync(taskId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskRepository.DeleteAllAsync(cancellationToken);
    }
}