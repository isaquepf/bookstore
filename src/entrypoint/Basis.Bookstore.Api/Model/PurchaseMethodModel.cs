namespace Basis.Bookstore.Api.Model
{
    public class PurchaseMethodModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int BookId { get; set; }
    }
}
