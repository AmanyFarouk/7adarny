using _7adarny.Application.Contracts.Interfaces;
using _7adarny.Domin.Entities;
using _7adarny.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Infrastructure.Reposiotries
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();

        }
        #region Create
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region SoftDelete
        public async Task Delete(int id)
        {
            var entity = await GetByIdWithTrackingAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt= DateTime.UtcNow;
            }
        } 
        #endregion

        #region Read
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => e.IsDeleted == false).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetByIdWithTrackingAsync(int id)
        {
            return await _dbSet.AsTracking().FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);
        }
        #endregion

        #region Update
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        } 
        #endregion

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
