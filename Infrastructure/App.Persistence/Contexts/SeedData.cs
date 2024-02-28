using App.Application.Features.Common;
using App.Domain.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Contexts;

internal class SeedData
{
    private static List<Guid>? userIds;
    private static List<Guid>? blogIds;
    private static List<Guid>? categoryIds;
    private static List<User> GetUsers()
    {
        var result = new Faker<User>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.FirstName, i => i.Person.FirstName)
            .RuleFor(i => i.LastName, i => i.Person.LastName)
            .RuleFor(i => i.EmailAddress, i => i.Internet.Email())
            .RuleFor(i => i.Password, i => PasswordEncryptor.Encrypt(i.Internet.Password()))
            .Generate(500);

        userIds = result.Select(i => i.Id).ToList();

        return result;
    }
    private static List<Blog> GetBlogs()
    {
        var result = new Faker<Blog>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.Content, i => i.Lorem.Paragraph(10))
            .RuleFor(i => i.Title, i => i.Lorem.Sentences(1))
            .RuleFor(i => i.UserId, i => i.PickRandom<Guid>(userIds))
            .Generate(150);

        blogIds = result.Select(i => i.Id).ToList();

        return result;
    }
    private static List<Category> GetCategories()
    {
        var result = new Faker<Category>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.CategoryName, i => i.Lorem.Word())
            .Generate(50);

        categoryIds = result.Select(i => i.Id).ToList();

        return result;
    }
    private static List<BlogsCategory> GetBlogCategories()
    {
        var result = new Faker<BlogsCategory>("tr")
            .RuleFor(i => i.Id, i => Guid.NewGuid())
            .RuleFor(i => i.CreatedDate, i => i.Date.Between(DateTime.Now.AddDays(-100), DateTime.Now))
            .RuleFor(i => i.BlogId, i => i.PickRandom<Guid>(blogIds))
            .RuleFor(i => i.CategoryId, i => i.PickRandom<Guid>(categoryIds))
            .Generate(120);

        categoryIds = result.Select(i => i.Id).ToList();

        return result;
    }
    public async Task SeedAsync(string connectionString)
    {
        var options = new DbContextOptionsBuilder<AppEfContext>();
        options.UseSqlite(connectionString);

        var context = new AppEfContext(options.Options);

        var users = GetUsers();
        var blogs = GetBlogs();
        var categories = GetCategories();
        var blogCategories = GetBlogCategories();

        await context.Users.AddRangeAsync(users);
        await context.Blogs.AddRangeAsync(blogs);
        await context.Categories.AddRangeAsync(categories);
        await context.BlogsCategories.AddRangeAsync(blogCategories);

        await context.SaveChangesAsync();
    }

}
