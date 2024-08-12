using System.Linq.Expressions;

namespace Basis.Bookstore.Core.Domain.Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> : IDisposable
    {
        T Add(T entity);

        void Remove(T entity);

        T GetById(int id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        void Update(T entity);

        void AddRange(List<T> entities);
        void RemoveRange(List<T> entities);
    }
}
