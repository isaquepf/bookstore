namespace Basis.Bookstore.Mvc.ViewModel
{
    public class PurchaseMethodViewModel
    {        
        public List<PurchaseMethodViewItemModel> PurchaseMethods { get; set; }
    }

    public class PurchaseMethodViewItemModel
    {

        public PurchaseMethodViewItemModel()
        {
            
        }
        public PurchaseMethodViewItemModel(int id, string name, decimal price = 0)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}
