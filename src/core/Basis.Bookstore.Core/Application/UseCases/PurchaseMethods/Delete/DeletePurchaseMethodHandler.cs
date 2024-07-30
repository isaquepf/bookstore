using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Delete
{
    public class DeleteSaleTypeHandler : Handler<DeletePurchaseMethodCommand>
    {
        private readonly IPurchaseMethodRepository _repository;
        private readonly ILogger<DeleteSaleTypeHandler> _logger;
        public DeleteSaleTypeHandler(IPurchaseMethodRepository repository, ILogger<DeleteSaleTypeHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public override Task<Result> Handle(DeletePurchaseMethodCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var purchaseMethod = _repository.GetById(request.Id);

                if (purchaseMethod == null)
                {
                    Result.AddNotification("Não foi encontrado uma forma de venda válida.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }


                _repository.Remove(purchaseMethod);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when find an author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }


            return Task.FromResult(Result);
        }
    }
}
