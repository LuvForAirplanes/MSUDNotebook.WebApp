using Candle.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MSUDTrack.Services
{
    public class Repository<TEntity, TId> where TEntity: class, new()
    {
        public TrackerDbContext _context;

        public bool IsSortDescending { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entity">Entity to be used at the top of the select list.</param>
        public Repository(TrackerDbContext context)
        {
            _context = context;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity, bool saveNow = true)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            if (saveNow)
                await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TId id, bool saveNow = true)
        {
            var entity = await GetByIdAsync(id);
            _context.Set<TEntity>().Remove(entity);
            if (saveNow)
                await _context.SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsAsync(TId id)
        {
            var x = await GetByIdAsync(id, true);
            return x != null;
        }

        public virtual IQueryable<TEntity> Get(bool disableTracking = true)
        {
            if (disableTracking)
                return _context.Set<TEntity>().AsNoTracking();
            else
                return _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id, bool disableTracking = true)
        {
            var e = await _context.Set<TEntity>().FindAsync(id);

            if (disableTracking && e != null)
                _context.Entry(e).State = EntityState.Detached;

            return e;               
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, TId id, bool saveNow = true)
        {
            _context.Set<TEntity>().Update(entity);
            if (saveNow)
                await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<TEntity>> ListAsync()
        {
            return await Get()
                .ToListAsync();

        }

        public virtual async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
