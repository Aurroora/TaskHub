namespace Logic.Tasks.Models;

/// <summary>
/// Модель задачи
/// </summary>
public sealed class TaskModel
{
    public Guid Id { get; }
    public string Title { get; }
    public Guid CreatedByUserId { get; }
    public DateTimeOffset CreatedUtc { get; }

    public TaskModel(Guid id, string title, Guid createdByUserId, DateTimeOffset createdUtc)
    {
        Id = id;
        Title = title;
        CreatedByUserId = createdByUserId;
        CreatedUtc = createdUtc;
    }
}