using System.Linq.Expressions;
using App.Application.Abstractions.Repositories;
using App.Domain.Entities.Common;
using App.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : class, IEntityBase, new()
{
    private readonly AppEfContext dbContext;
    public Repository(AppEfContext dbContext)
    {
        this.dbContext = dbContext;
    }
    private DbSet<T> Table => dbContext.Set<T>();
    public IQueryable<T> AsQueryable()
    {
        return Table.AsQueryable();
    }
    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;

        if (predicate != null) query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var item in includeProperties) query = query.Include(item);

        return await query.ToListAsync();
    }
    public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = Table;
        query = query.Where(predicate);

        if (includeProperties.Any())
            foreach (var item in includeProperties) query = query.Include(item);

        return (await query.FirstOrDefaultAsync())!;
    }
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        return await Table.AnyAsync(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate != null) return await Table.CountAsync(predicate);
        else return await Table.CountAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));
    }
    public async Task<T> GetByGuidAsync(Guid id)
    {
        return (await Table.FindAsync(id))!;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        await Task.Run(() => Table.Update(entity));
        return entity;
    }
    public async Task<int> SaveAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}