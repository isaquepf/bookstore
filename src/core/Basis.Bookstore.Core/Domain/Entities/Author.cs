using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class Author : EntityBase
    {
        public  string Name { get; set; }       
        public virtual List<BookAuthor> BookAuthors { get; set; }
    }
}
