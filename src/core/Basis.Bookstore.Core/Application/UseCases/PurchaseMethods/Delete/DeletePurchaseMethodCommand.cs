using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Delete
{
    public class DeletePurchaseMethodCommand : Command<DeletePurchaseMethodCommand>
    {
        public int Id { get; set; }

        public DeletePurchaseMethodCommand()
        {

        }
        public DeletePurchaseMethodCommand(int id)
        {
            Id = id;
        }
    }
}
