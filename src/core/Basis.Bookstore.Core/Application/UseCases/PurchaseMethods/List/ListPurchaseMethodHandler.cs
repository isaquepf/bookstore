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
            var results = new List<PurchaseMethodResult>();

            var purchaseMethods = _repository.GetAll();

            Result.Data = results.Select(p => new PurchaseMethodResult(p.Id, p.Name));

            return Task.FromResult(Result);
        }
    }
}
