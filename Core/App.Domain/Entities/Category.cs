using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Category : EntityBase
{
    public string? CategoryName { get; set; }
    public ICollection<BlogsCategory>? BlogsCategories { get; set; }
}
