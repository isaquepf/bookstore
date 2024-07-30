using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BasisBookstore.Infraestructure.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(BookstoreContext contexto, ILogger<RepositoryBase<Book>> logger) : base(contexto, logger)
        {
        }

        public override IEnumerable<Book> GetAll()
        {
            return Entity.AsNoTracking().Select(c => c)
                        .Include(c => c.BookAuthors)
                        .ThenInclude(c => c.Author)
                        .Include(c => c.BookSubjects)
                        .ThenInclude(c => c.Subject)
                        .Include(c => c.BookPurchaseMethods)
                        .ThenInclude(c => c.PurchaseMethod)
                                .ToList();
        }
    }
}
