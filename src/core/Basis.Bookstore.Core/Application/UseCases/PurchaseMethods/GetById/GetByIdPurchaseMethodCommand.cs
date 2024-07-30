using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.FindById
{
    public class GetByIdPurchaseMethodCommand : Command<GetByIdPurchaseMethodCommand>
    {
        public int Id { get; set; }


        public GetByIdPurchaseMethodCommand()
        {

        }
        public GetByIdPurchaseMethodCommand(int id)
        {
            Id = id;
        }

    }
}
