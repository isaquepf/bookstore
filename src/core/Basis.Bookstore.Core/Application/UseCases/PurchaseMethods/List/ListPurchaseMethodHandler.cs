using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Find
{
    public class ListPurchaseMethodCommandHandler : Handler<ListPurchaseMethodCommand>
    {
        private readonly IPurchaseMethodRepository _repository;
        public ListPurchaseMethodCommandHandler(IPurchaseMethodRepository repository)
        {
            _repository = repository;
        }


        public override Task<Result> Handle(ListPurchaseMethodCommand request, CancellationToken cancellationToken)
        {
            Result.Data = _repository.GetAll();

            return Task.FromResult(Result);
        }
    }
}
