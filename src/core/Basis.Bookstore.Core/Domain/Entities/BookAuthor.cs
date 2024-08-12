using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class BookAuthor : EntityBase
    {
        public BookAuthor()
        {

        }

        public BookAuthor(int bookId, int authorId)
        {
            BookId = bookId;
            AuthorId = authorId;
        }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }
    }
}
