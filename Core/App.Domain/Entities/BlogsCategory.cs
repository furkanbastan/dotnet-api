using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class BlogsCategory : EntityBase
{
    public Guid BlogId { get; set; }
    public Blog? Blog { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}
