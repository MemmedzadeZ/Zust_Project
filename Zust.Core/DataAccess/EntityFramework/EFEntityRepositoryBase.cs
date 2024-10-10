using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zust.Core.Entities;

namespace Zust.Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public async Task Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Set<TEntity>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
            }
        }

        public async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            }
        }

        public async Task SaveAll()
        {
            using (var context = new TContext())
            {
                await context.SaveChangesAsync();
            }
        }
 
    }
}
