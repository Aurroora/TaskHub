using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories;

public sealed class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;

    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> CreateAsync(string title, Guid userId, CancellationToken cancellationToken)
    {
        var task = new TaskEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            CreatedByUserId = userId,
            CreatedUtc = DateTimeOffset.UtcNow
        };

        await _dbContext.Tasks.AddAsync(task, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return task;
    }

    public async Task<List<TaskEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<bool> UpdateTitleAsync(Guid id, string newTitle, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (task == null) return false;

        task.Title = newTitle;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if (task == null) return false;

        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DeleteAllAsync(CancellationToken cancellationToken)
    {
        await _dbContext.Tasks.ExecuteDeleteAsync(cancellationToken);
    }
}