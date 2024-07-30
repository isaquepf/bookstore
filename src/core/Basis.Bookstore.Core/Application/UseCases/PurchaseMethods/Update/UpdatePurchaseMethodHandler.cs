using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Update;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;


namespace MyBook.Application.UseCases.SaleType.Update
{
    public class UpdatePurchaseMethodHandler : Handler<UpdatePurchaseMethodCommand>
    {
       
        private readonly IPurchaseMethodRepository _repository;
        public UpdatePurchaseMethodHandler(IPurchaseMethodRepository repository)
        {
            _repository = repository;
        }

        public override Task<Result> Handle(UpdatePurchaseMethodCommand request, CancellationToken cancellationToken)
        {
            var purchaseMethod = _repository.GetById(request.Id);

            purchaseMethod.Name = request.PurchaseMethod.Description;
            
            _repository.Update(purchaseMethod);

            return Task.FromResult(Result);
        }
    }
}
