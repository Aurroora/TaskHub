namespace Api.Controllers.Tasks.Response;

/// <summary>
/// Ответ с данными задачи
/// </summary>
public record TaskResponse
{
    /// <summary>
    /// Идентификатор задачи
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Название задачи
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Идентификатор пользователя, создавшего задачу
    /// </summary>
    public Guid CreatedByUserId { get; }

    /// <summary>
    /// Дата и время создания задачи в UTC
    /// </summary>
    public DateTimeOffset CreatedUtc { get; }

    /// <summary>
    /// Создает ответ с данными задачи
    /// </summary>
    /// <param name="id">Идентификатор задачи</param>
    /// <param name="title">Название задачи</param>
    /// <param name="createdByUserId">Идентификатор пользователя, создавшего задачу</param>
    /// <param name="createdUtc">Дата и время создания задачи в UTC</param>
    public TaskResponse(Guid id, string title, Guid createdByUserId, DateTimeOffset createdUtc)
    {
        Id = id;
        Title = title;
        CreatedByUserId = createdByUserId;
        CreatedUtc = createdUtc;
    }
}