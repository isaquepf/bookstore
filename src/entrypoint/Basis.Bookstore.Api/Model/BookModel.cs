namespace Basis.Bookstore.Api.Model
{
    public class BookModel
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Publisher { get; set; }
        public required int Edition { get; set; }
        public required string PublishedYear { get; set; }
        public List<int> AuthorsIds { get; set; }
        public List<int> SubjectsIds { get; set; }
        public List<int> PurchaseMethods { get; set; }
    }
}
