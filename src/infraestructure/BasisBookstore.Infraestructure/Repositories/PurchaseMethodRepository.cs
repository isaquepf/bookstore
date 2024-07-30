using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace BasisBookstore.Infraestructure.Repositories
{
    public class PurchaseMethodRepository : RepositoryBase<PurchaseMethod>, IPurchaseMethodRepository
    {
        public PurchaseMethodRepository(BookstoreContext contexto, ILogger<RepositoryBase<PurchaseMethod>> logger) : base(contexto, logger)
        {
        }
    }
}
