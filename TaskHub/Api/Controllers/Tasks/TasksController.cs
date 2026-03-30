using Api.Controllers.Tasks.Request;
using Api.Controllers.Tasks.Response;
using Api.UseCases.Tasks.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Api.Attributes;

namespace Api.Controllers.Tasks;

/// <summary>
/// Контроллер работы с задачами
/// </summary>
[ApiController]
[Route("tasks")]
[ResponseTimeHeader]
[StudentInfoHeaders]
public sealed class TasksController : ControllerBase
{
    private readonly IManageTaskUseCase _taskUseCase;

    public TasksController(IManageTaskUseCase taskUseCase)
    {
        _taskUseCase = taskUseCase;
    }

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

    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetAllTasksAsync(CancellationToken cancellationToken)
    {
        var response = await _taskUseCase.GetAllTasksAsync(cancellationToken);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetTaskByIdAsync(
        [FromRouteTaskId] Guid id,
        CancellationToken cancellationToken)
    {
        var taskResponse = await _taskUseCase.GetTaskByIdAsync(id, cancellationToken);
        if (taskResponse is null)
        {
            return NotFound();
        }
        return Ok(taskResponse);
    }

    [HttpPut("{id}/title")]
    [ValidateUserRequest]
    public async Task<IActionResult> SetTaskTitleAsync(
        [FromRouteTaskId] Guid id,
        [FromBody] SetTaskTitleRequest? request,
        CancellationToken cancellationToken)
    {
        await _taskUseCase.SetTaskTitleAsync(id, request!.Title ?? string.Empty, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTaskByIdAsync(
        [FromRouteTaskId] Guid id,
        CancellationToken cancellationToken)
    {
        var deleted = await _taskUseCase.DeleteTaskByIdAsync(id, cancellationToken);
        if (deleted == false)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAllTasksAsync(CancellationToken cancellationToken)
    {
        await _taskUseCase.DeleteAllTasksAsync(cancellationToken);
        return NoContent();
    }
}