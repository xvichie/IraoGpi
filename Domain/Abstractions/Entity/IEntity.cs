namespace IraoGpi.Domain.Abstractions.Entity;

public interface IEntity
{
    DateTimeOffset DateCreated { get; init; }
    DateTimeOffset? DateModified { get; set; }
}
