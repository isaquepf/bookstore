using Basis.Bookstore.Core.Domain.Contracts;

namespace Basis.Bookstore.Core.Domain.Entities
{
    public class PurchaseMethod : EntityBase
    {
        public string Name { get; set; }
        public virtual List<BookPurchaseMethod> BookPurchaseMethods { get; set; }
    }
}
