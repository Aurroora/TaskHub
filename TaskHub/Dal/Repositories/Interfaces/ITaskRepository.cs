using Dal.Entities;

namespace Dal.Repositories;

public interface ITaskRepository
{
    Task<TaskEntity> CreateAsync(string title, Guid userId, CancellationToken cancellationToken);

    Task<List<TaskEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> UpdateTitleAsync(Guid id, string newTitle, CancellationToken cancellationToken);

    Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task DeleteAllAsync(CancellationToken cancellationToken);
}