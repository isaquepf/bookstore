using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class Subject : EntityBase
    {
        public required string Description { get; set; }

        public virtual List<Book> Books { get; set; }

        public ICollection<BookSubject> BookSubjects { get; set; }
    }
}
