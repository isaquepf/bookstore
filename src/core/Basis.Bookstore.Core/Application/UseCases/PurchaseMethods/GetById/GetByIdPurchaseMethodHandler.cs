using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;


namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.FindById
{
    public class GetByIdPurchaseMethodHandler : Handler<GetByIdPurchaseMethodCommand>
    {

        private readonly IPurchaseMethodRepository _repository;
        public GetByIdPurchaseMethodHandler(IPurchaseMethodRepository repository)
        {
            _repository = repository;
        }

        public override Task<Result> Handle(GetByIdPurchaseMethodCommand request, CancellationToken cancellationToken)
        {
            Result.Data = _repository.GetById(request.Id);

            return Task.FromResult(Result);
        }
    }
}
