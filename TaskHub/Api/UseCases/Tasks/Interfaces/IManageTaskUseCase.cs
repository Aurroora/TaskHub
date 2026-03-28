using Api.Controllers.Tasks.Response;

namespace Api.UseCases.Tasks.Interfaces;

public interface IManageTaskUseCase
{
    /// <summary>
    /// Выполнить создание задачи
    /// </summary>
    /// <param name="title">Название задачи</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель задачи</returns>
    Task<TaskResponse> CreateTaskAsync(string title, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Выполнить получение всех задач
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список задач</returns>
    Task<TaskListResponse> GetAllTasksAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Выполнить получение задачи
    /// </summary>
    /// <param name="taskId">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель задачи или null, если задача не найдена</returns>
    Task<TaskResponse?> GetTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);

    /// <summary>
    /// Выполнить изменение названия задачи
    /// </summary>
    /// <param name="taskId">Идентификатор задачи</param>
    /// <param name="title">Новое название</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task SetTaskTitleAsync(Guid taskId, string title, CancellationToken cancellationToken);

    /// <summary>
    /// Выполнить удаление всех задач
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    Task DeleteAllTasksAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Выполнить удаление задачи
    /// </summary>
    /// <param name="taskId">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>true, если задача удалена, иначе false</returns>
    Task<bool> DeleteTaskByIdAsync(Guid taskId, CancellationToken cancellationToken);
}