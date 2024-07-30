using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Create;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Update
{
    public class UpdatePurchaseMethodCommand : Command<UpdatePurchaseMethodCommand>
    {
        public int Id { get; set; }
        public CreatePurchaseMethodCommand PurchaseMethod { get; set; }


        public UpdatePurchaseMethodCommand()
        {
        }
        public UpdatePurchaseMethodCommand(int id, CreatePurchaseMethodCommand purchaseMethod)
        {
            Id = id;
            PurchaseMethod = purchaseMethod;
        }

    }
}
