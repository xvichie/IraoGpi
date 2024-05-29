using IraoGpi.Domain.Enums;

namespace IraoGpi.Domain.Abstractions.Entity;

public interface IEntityStatus : IEntity
{
    EntityStatus EntityStatus { get; set; }
}
