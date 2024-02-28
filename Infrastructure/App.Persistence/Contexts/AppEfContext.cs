using System.Reflection;
using App.Domain.Entities;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.Persistence.Contexts;

public class AppEfContext : DbContext
{
    public AppEfContext(DbContextOptions<AppEfContext> options) : base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.Entries()
            .Where(i => i.State == EntityState.Added)
            .Select(i => (EntityBase)i.Entity)
            .ToList()
            .ForEach(i => i.CreatedDate = DateTime.Now);

        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<BlogsCategory> BlogsCategories { get; set; }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppEfContext>
{
    public AppEfContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppEfContext> dbContextOptionsBuilder = new();
        //buraya geliştirme ortamındaki veri tabanı sabit olarak yazılmasında sakınca yoktur! bu connection string çalışma anında değil sadece tasarım zamanında
        //yani "dotnet ef" komutuyla migration oluştururken ve veri tabanınına bu migrationları uygularken kullanılır!!
        //çalışma zamanında kullanılan connection string AddDbContext metodu ile geçirilmelidir !!
        dbContextOptionsBuilder.UseSqlite("Data Source = C:\\Users\\bbast\\OneDrive\\Masaüstü\\dotnet-api\\Infrastructure\\App.Persistence\\Database\\App.db;");
        return new(dbContextOptionsBuilder.Options);
    }
}
