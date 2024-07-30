using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using BasisBookstore.Infraestructure.Contexts;
using BasisBookstore.Infraestructure.Repositories.Base;
using Microsoft.Extensions.Logging;

namespace BasisBookstore.Infraestructure.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(BookstoreContext contexto, ILogger<RepositoryBase<Author>> logger) 
            : base(contexto, logger) {}
    }
}
