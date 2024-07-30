using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace BasisBookstore.Infraestructure.Repositories
{
    public class BookPurchaseMethodRepository : RepositoryBase<BookPurchaseMethod>, IBookPurchaseMethodRepository
    {
        public BookPurchaseMethodRepository(BookstoreContext contexto, ILogger<RepositoryBase<BookPurchaseMethod>> logger) : base(contexto, logger)
        {
        }
    }
}
