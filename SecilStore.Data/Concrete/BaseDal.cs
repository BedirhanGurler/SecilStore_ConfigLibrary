using Microsoft.EntityFrameworkCore;
using SecilStore.Data.BaseEntity;
using SecilStore.Data.Context;
using System.Linq.Expressions;

namespace SecilStore.Data.Concrete
{
    public abstract class BaseDal<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private ConfigDbContext? _db;
        protected virtual ConfigDbContext db
        {
            get
            {
                return _db ??= new ConfigDbContextFactory().CreateDbContext();
            }
        }
        public virtual IEnumerable<TEntity> Select()
        {
            return ExecuteSelect(null);
        }

        public virtual TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            return db.Set<TEntity>().FirstOrDefault(filter);
        }

        public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await db.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<bool> HasAnyData(Expression<Func<TEntity, bool>> filter)
        {
            return await db.Set<TEntity>().AnyAsync(filter);
        }

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>>[] predicates)
        {
            return ExecuteSelect(predicates);
        }

        public virtual async Task<IEnumerable<TEntity>> SelectAsync(params Expression<Func<TEntity, bool>>[] predicates)
        {
            IQueryable<TEntity> query = db.Set<TEntity>().AsNoTracking();

            if (predicates != null && predicates.Length != 0)
            {
                foreach (var predicate in predicates)
                {
                    query = query.Where(predicate);
                }
            }

            return await query.ToListAsync();
        }

        public IEnumerable<TEntity> GetAllWithEagerLoad(string[] children)
        {
            return ExecuteGetAll(null, children);
        }

        public IEnumerable<TEntity> GetAllWithEagerLoad(Expression<Func<TEntity, bool>>[]? predicates, string[] children)
        {
            return ExecuteGetAll(predicates, children);
        }

        public virtual TEntity? SelectWithId(TKey key)
        {
            TEntity? entity = null;
            entity = db.Set<TEntity>().Find(key);
            return entity;
        }

        public virtual async Task<TEntity?> SelectWithIdAsync(TKey key)
        {
            TEntity? entity = null;
            entity = await db.Set<TEntity>().FindAsync(key);
            return entity;
        }

        public virtual TEntity Insert(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await db.Set<TEntity>().AddAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<object>> InsertListAsync(List<object> entity)
        {
            await db.Set<object>().AddRangeAsync(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public virtual TEntity? Update(TKey? key, TEntity entity)
        {
            var tmp = db.Set<TEntity>().Find(key);

            if (tmp == null)
                return null;

            db.Entry(tmp).State = EntityState.Modified;
            db.Entry(tmp).CurrentValues.SetValues(entity);

            db.SaveChanges();

            return entity;
        }

        public virtual async Task<TEntity?> UpdateAsync(TKey? key, TEntity entity)
        {
            var tmp = await db.Set<TEntity>().FindAsync(key);

            if (tmp == null)
                return null;

            db.Entry(tmp).State = EntityState.Modified;
            db.Entry(tmp).CurrentValues.SetValues(entity);

            await db.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> DeleteAsync(TKey key)
        {
            var tmp = db.Set<TEntity>().Find(key);

            if (tmp == null)
                return false;

            db.Remove(tmp);
            db.Entry(tmp).State = EntityState.Deleted;

            return await db.SaveChangesAsync() > 0;
        }

        //public virtual async Task<bool> PacifyAsync(object key)
        //{
        //    var tmp = db.Set<TEntity>().Find(key);

        //    if (tmp == null)
        //        return false;

        //    tmp.IsActive = false;

        //    db.Entry(tmp).State = EntityState.Modified;

        //    return await db.SaveChangesAsync() > 0;
        //}

        public virtual async Task<int> GetNumberOfRecords(Expression<Func<TEntity, bool>>? predicate)
        {
            var queryable = db.Set<TEntity>().AsNoTracking().AsQueryable();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.CountAsync();
        }

        private IEnumerable<TEntity> ExecuteSelect(Expression<Func<TEntity, bool>>[]? predicates)
        {
            var queryable = db.Set<TEntity>().AsNoTracking().AsQueryable();

            if (predicates != null)
            {
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);
            }

            return queryable.ToList();
        }

        private async Task<IEnumerable<TEntity>> ExecuteSelectAsync(Expression<Func<TEntity, bool>>[]? predicates)
        {
            var queryable = db.Set<TEntity>().AsNoTracking().AsQueryable();

            if (predicates != null)
            {
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);
            }

            return await queryable.ToListAsync();
        }

        private IEnumerable<TEntity> ExecuteGetAll(Expression<Func<TEntity, bool>>[]? predicates, string[] children)
        {
            var queryable = db.Set<TEntity>().AsNoTracking().AsQueryable();

            if (predicates != null)
                foreach (var predicate in predicates)
                    queryable = queryable.Where(predicate);

            foreach (string entity in children)
                queryable = queryable.Include(entity);

            return queryable.ToList();
        }
    }
}
