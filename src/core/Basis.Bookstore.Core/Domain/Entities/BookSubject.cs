using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class BookSubject : EntityBase
    {
        public BookSubject() {}

        public BookSubject(int bookId, int subjectId)
        {
            BookId = bookId;
            SubjectId = subjectId;
        }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
