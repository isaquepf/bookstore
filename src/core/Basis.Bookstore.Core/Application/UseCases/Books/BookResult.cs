namespace Basis.Bookstore.Core.Application.UseCases.Books
{
    public class BookResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }
        public string PublishedYear { get; set; }
        public List<AuthorResult> Authors { get; set; }

        public List<SubjectResult> Subjects { get; set; }

        public List<PurchaseMethodResult> PurchaseMethods { get; set; }
        public string AuthorsText { get; set; }
        public string SubjectsText { get; set; }

    }

    public class SubjectResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
    }

    public class PurchaseMethodResult
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int BookId { get; set; }
    }

    public class AuthorResult
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
