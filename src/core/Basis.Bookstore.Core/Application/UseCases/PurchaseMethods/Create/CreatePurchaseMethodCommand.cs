using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Create
{
    public class CreatePurchaseMethodCommand : Command<CreatePurchaseMethodCommand>
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BookId { get; set; }
        public int Id { get; set; }
        public CreatePurchaseMethodCommand() {}

        public CreatePurchaseMethodCommand(string description, decimal price, int bookId)
        {
            Description = description;
            Price = price;
            BookId = bookId;
        }
    }
}
