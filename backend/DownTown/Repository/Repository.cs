﻿using DownTown.Context;
using Microsoft.EntityFrameworkCore;

namespace DownTown.Repository;

public interface IRepository<T>
{
    IQueryable<T> GetAll();
    Task<T> GetById(int id);
    Task<bool> Create(T data);
    bool Delete(T data);
    Task<bool> DeleteById(int id);
    bool Update(T data);
}

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public IQueryable<T> GetAll()
    {
        var query = _dbSet.AsQueryable();
        return query;
    }

    public async Task<T> GetById(int id)
    {
        return await _dbSet.FindAsync(id) ?? throw new NullReferenceException();
    }

    public async Task<bool> Create(T data)
    {
        await _dbSet.AddAsync(data);
        return true;
    }

    public bool Delete(T data)
    {
        var entityEntry = _dbSet.Remove(data);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> DeleteById(int id)
    {
        var data = await _dbSet.FindAsync(id);
        return data != null && Delete(data);
    }

    public bool Update(T data)
    {
        var entityEntry = _dbSet.Update(data);
        return entityEntry.State == EntityState.Modified;
    }
}