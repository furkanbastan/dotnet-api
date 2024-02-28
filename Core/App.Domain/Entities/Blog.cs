using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Blog : EntityBase
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public ICollection<BlogsCategory>? BlogsCategories { get; set; }
}
