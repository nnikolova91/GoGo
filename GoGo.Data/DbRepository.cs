using GoGo.Data.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoGo.Data
{
    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly GoDbContext context;

        private DbSet<TEntity> dbSet;

        public DbRepository(GoDbContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }
        
        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public Task AddRangeAsync(List<TEntity> entitis)
        {
            return this.dbSet.AddRangeAsync(entitis);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
