using IraoGpi.Domain.Abstractions.Entity;
using IraoGpi.Domain.Enums;
using TaskStatus = IraoGpi.Domain.Enums.TaskStatus;

namespace IraoGpi.Domain.Entities;

public class Task : IEntityStatus
{
    public int Id { get; init; }

    public string Title { get; private set; }

    public string Description { get; private set; }

    public TaskPriority Priority { get; private set; }

    public TaskStatus Status { get; private set; }

    public EntityStatus EntityStatus { get; set; }

    public int MemberId { get; private set; }

    public DateTimeOffset DateCreated { get; init; }

    public DateTimeOffset? DateModified { get; set; }

    public static Task Create(string title, string description, TaskPriority priority, int userId)
    {
        return new Task(title, description, priority, TaskStatus.New, userId, EntityStatus.Active);
    }

    private Task(string title, string description, TaskPriority priority, TaskStatus status, int memberId,
        EntityStatus entityStatus)
    {
        Title = title;
        Description = description;
        Priority = priority;
        Status = status;
        MemberId = memberId;
        EntityStatus = entityStatus;
    }

    public Task()
    {

    }

    public void MarkAsDeleted()
    {
        EntityStatus = EntityStatus.Deleted;
    }

    public void SetStatus(TaskStatus status)
    {
        Status = status;
    }

    public virtual Member Member { get; private set; }
}
