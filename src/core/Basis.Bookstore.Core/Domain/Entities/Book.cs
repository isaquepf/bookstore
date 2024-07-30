using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class Book : EntityBase
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Publisher { get; set; }
        public required int Edition { get; set; }
        public required string PublishedYear { get; set; }
        public virtual List<BookAuthor> BookAuthors { get; set; }
        public virtual List<BookSubject> BookSubjects { get; set; }
        public virtual List<BookPurchaseMethod> BookPurchaseMethods { get; set; }        
    }
}
