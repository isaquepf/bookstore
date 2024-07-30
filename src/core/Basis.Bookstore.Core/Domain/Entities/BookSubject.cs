using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class BookSubject : EntityBase
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
