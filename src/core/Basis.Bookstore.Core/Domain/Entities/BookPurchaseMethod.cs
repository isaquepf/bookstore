using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class BookPurchaseMethod : EntityBase
    { 
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int PurchaseMethodId { get; set; }
        public PurchaseMethod PurchaseMethod { get; set; }
        public decimal Price { get; set; }
    }

}
