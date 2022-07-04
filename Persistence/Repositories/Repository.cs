﻿using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Repositories.Interfaces;

namespace Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;
        public Repository(BowlingDbContext dbContext) => _entities = dbContext.Set<TEntity>();
        public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);
        public void Delete(TEntity entity) => _entities.Remove(entity);
        public void DeleteList(IEnumerable<TEntity> entity) => _entities.RemoveRange(entity);
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await Task.FromResult(_entities.AsEnumerable());
        public virtual async Task<TEntity> GetByIdAsync(Guid id) => await _entities.FindAsync(id);
        public async Task UpdateAsync(TEntity entity) => await Task.CompletedTask;
    }
}
