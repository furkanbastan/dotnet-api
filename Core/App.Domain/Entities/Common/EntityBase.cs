namespace App.Domain.Entities.Common;

public class EntityBase : IEntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
}
