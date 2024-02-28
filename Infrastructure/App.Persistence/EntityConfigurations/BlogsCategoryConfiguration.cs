using App.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EntityConfigurations;

public class BlogsCategoryConfiguration : EntityBaseConfiguration<BlogsCategory>
{
    public override void Configure(EntityTypeBuilder<BlogsCategory> builder)
    {
        base.Configure(builder);
    }
}
