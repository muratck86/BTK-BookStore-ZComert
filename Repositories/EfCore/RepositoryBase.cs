using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EfCore
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public void Delete(T entity) => _context.Set<T>().Remove(entity);

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>()
                : _context.Set<T>().AsNoTracking();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? _context.Set<T>().Where(expression)
                : _context.Set<T>().Where(expression).AsNoTracking();
        }

        public T? GetOne(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().SingleOrDefault(expression);
        }

        public void Update(T entity) => _context.Set<T>().Update(entity);
    }
}
