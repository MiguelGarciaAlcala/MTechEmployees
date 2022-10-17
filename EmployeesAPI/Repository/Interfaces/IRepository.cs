using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Create(T entity);
        Task CreateAsync(T entity);

        T? Get(params object[] id);
        Task<T?> GetAsync(params object[] id);

        T? Get(Expression<Func<T, bool>> expression);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, List<string>? includes = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, List<string>? includes = null);

        void Delete(params object[] id);

        int Save();
        Task<int> SaveAsync();
    }
}
