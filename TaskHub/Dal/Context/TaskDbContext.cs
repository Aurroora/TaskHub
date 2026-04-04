using Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context;

/// <summary>
/// Контекст базы данных для работы с задачами
/// </summary>
public sealed class TaskDbContext : DbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.ToTable("tasks");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title).IsRequired();

            entity.HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}