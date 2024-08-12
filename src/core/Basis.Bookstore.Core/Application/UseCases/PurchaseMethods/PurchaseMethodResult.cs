namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods
{
    public class PurchaseMethodResult
    {
        public PurchaseMethodResult()
        {
            
        }
        public PurchaseMethodResult(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
