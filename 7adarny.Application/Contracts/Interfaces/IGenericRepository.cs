using _7adarny.Domin.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7adarny.Application.Contracts.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);

        Task<T?> GetByIdWithTrackingAsync(int id);

        Task<IReadOnlyList<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);

        Task Delete(int id);

        Task SaveChangesAsync();
    }
}
