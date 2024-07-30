using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace BasisBookstore.Infraestructure.Repositories
{
    public class BookSubjectRepository : RepositoryBase<BookSubject>, IBookSubjectRepository
    {
        public BookSubjectRepository(BookstoreContext contexto, ILogger<RepositoryBase<BookSubject>> logger) : base(contexto, logger)
        {
        }
    }
}
