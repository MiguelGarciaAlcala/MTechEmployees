using DataAccess;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public void Create(T entity)
        {
            if(entity != null) 
                _entities.Add(entity);
        }

        public async Task CreateAsync(T entity)
        {
            if (entity != null)
                await _entities.AddAsync(entity);
        }

        public T? Get(params object[] id)
        {
            return _entities.Find(id);
        }

        public async Task<T?> GetAsync(params object[] id)
        {
            return await _entities.FindAsync(id);
        }

        public T? Get(Expression<Func<T, bool>> expression)
        {
            return _entities
                .Where(expression)
                .FirstOrDefault();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _entities
                .Where(expression)
                .FirstOrDefaultAsync();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? expression = null, List<string>? includes = null)
        {
            var queryable = _entities.AsQueryable();

            if (expression != null)
                queryable = queryable.Where(expression);

            if(includes != null)
            {
                foreach(var include in includes)
                    queryable = queryable.Include(include);
            }

            return queryable.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, List<string>? includes = null)
        {
            var queryable = _entities.AsQueryable();

            if (expression != null)
                queryable = queryable.Where(expression);

            if (includes != null)
            {
                foreach (var include in includes)
                    queryable = queryable.Include(include);
            }

            return await queryable.ToListAsync();
        }

        public void Delete(params object[] id)
        {
            var entity = _entities.Find(id);

            if (entity != null)
                _entities.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
