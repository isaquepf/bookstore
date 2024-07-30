using System.Linq.Expressions;

namespace Basis.Bookstore.Core.Domain.Contracts.Repositories.Base
{
    public interface IRepositoryBase<T> : IDisposable
    {
        T Add(T entidade);

        void Remove(T entidade);

        T GetById(int Id);

        IEnumerable<T> GetAll();

        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        void Update(T entidade);        
    }
}
