using System.Linq.Expressions;

namespace Repositories.Contracts
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll(bool trackChanges);

        IQueryable<T> GetByCondition(Expression<Func<T,bool>> expression, bool trackChanges);

        T? GetOne(Expression<Func<T,bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
