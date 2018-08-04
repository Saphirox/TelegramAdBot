using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TelegramAdBot.Entities;

namespace TelegramAdBot.DataAccess
{
    public interface IRepositoryBase<T, TKey>
        where T : class, IEntity<TKey>, new()
    {
        Task<T> GetByIdAsync(T entity);
        Task<T> GetByIdAsync(string id);
        Task<T> AddAsync(T entity);

        void AddAsync(IEnumerable<T> entities);

        void UpdateAsync(T c);
        void UpdateAsync(IEnumerable<T> entities);
        Task DeleteAsync(string id);

        Task DeleteAsync(T entity);

        Task DeleteAsync(Expression<Func<T, bool>> predicate);
        Task<long> CountAsync();
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
