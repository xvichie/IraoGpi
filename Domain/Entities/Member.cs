using IraoGpi.Domain.Abstractions.Entity;
using IraoGpi.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IraoGpi.Domain.Entities;

public class Member : IdentityUser<int>, IEntityStatus
{
    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public EntityStatus EntityStatus { get; set; }

    public DateTimeOffset DateCreated { get; init; }

    public DateTimeOffset? DateModified { get; set; }

    public void MarkAsDeleted()
    {
        EntityStatus = EntityStatus.Deleted;
    }

    public static Member Create(string firstName, string lastName, string userName)
    {
        return new Member(firstName, lastName, userName, EntityStatus.Active);
    }

    private Member(string firstName, string lastName, string userName, EntityStatus entityStatus) : base(userName)
    {
        FirstName = firstName;
        LastName = lastName;
        EntityStatus = entityStatus;
    }

    public Member()
    {

    }

    public virtual ICollection<Task> Tasks { get; private set; }
}
