using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Tasks;

using Api.Attributes;

/// <summary>
/// Контроллер работы с задачами
/// </summary>
[ApiController]
[Route("tasks")]
[ResponseTimeHeader]
[StudentInfoHeaders]
public sealed class TasksController : ControllerBase
{
    /// <summary>
    /// UseCase управления задачами
    /// </summary>
    private readonly IManageTaskUseCase _taskUseCase;

    public TasksController(IManageTaskUseCase taskUseCase)
    {
        _taskUseCase = taskUseCase;
    }

    /// <summary>
    /// Создать задачу
    /// </summary>
    /// <param name="request">Данные для создания задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная задача</returns>
    [HttpPost]
    [ValidateUserRequest]
    public async Task<ActionResult<TaskResponse>> CreateTaskAsync(
        [FromBody] CreateTaskRequest? request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse("3432da03-671d-4c4a-ac48-2f1f9039a47a");

        var task = await _taskUseCase.CreateTaskAsync(request!.Title ?? string.Empty, userId, cancellationToken);
        return StatusCode(201, task);
    }

    /// <summary>
    /// Получить все задачи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список задач</returns>
    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var response = await _taskUseCase.GetAllTasksAsync(cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Получить задачу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Задача или 404, если задача не найдена</returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var taskResponse = await _taskUseCase.GetTaskByIdAsync(id, cancellationToken);

        if (taskResponse is null)
        {
            return NotFound();
        }

        return Ok(taskResponse);
    }

    /// <summary>
    /// Изменить название задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="request">Данные для изменения названия</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе или 400 при неверном запросе</returns>
    [HttpPut("{id:guid}/title")]
    [ValidateUserRequest]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRoute] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _taskUseCase.SetTaskTitleAsync(id, request!.Title ?? string.Empty, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Удалить задачу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе или 404, если задача не найдена</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTaskByIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskUseCase.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить все задачи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>204 при успехе</returns>
    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await


_taskUseCase.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}