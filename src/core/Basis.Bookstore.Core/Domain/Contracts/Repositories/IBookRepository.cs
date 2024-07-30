using Basis.Bookstore.Core.Domain.Contracts.Repositories.Base;
using Basis.Bookstore.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basis.Bookstore.Core.Domain.Contracts.Repositories
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
    }
}
