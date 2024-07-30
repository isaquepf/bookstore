using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Create;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.SaleType.Create
{
    public class CreatePurchaseMethodHandler : Handler<CreatePurchaseMethodCommand>
    {
        private readonly IPurchaseMethodRepository _purchaseMethodRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookPurchaseMethodRepository _bookPurchaseMethodRepository;
        private readonly ILogger<CreatePurchaseMethodHandler> _logger;
        public CreatePurchaseMethodHandler(
            IPurchaseMethodRepository purchaseMethodRepository,
            IBookRepository bookRepository,
            IBookPurchaseMethodRepository bookPurchaseMethodRepository,
            ILogger<CreatePurchaseMethodHandler> logger)
        {
            _purchaseMethodRepository = purchaseMethodRepository;
            _bookRepository = bookRepository;
            _bookPurchaseMethodRepository = bookPurchaseMethodRepository;
            _logger = logger;
        }

        public override Task<Result> Handle(CreatePurchaseMethodCommand request, CancellationToken cancellationToken)
        {

            try
            {
                PurchaseMethod purchaseMethod = null;

                var bookEntity = _bookRepository.GetById(request.BookId);

                if (bookEntity == null)
                {
                    Result.AddNotification("O livro não foi encontrado.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                var purchaseMethods = _purchaseMethodRepository.Get(p => p.Name == request.Description);
               
                if (purchaseMethods != null && purchaseMethods.Any())
                {
                    purchaseMethod = purchaseMethods.FirstOrDefault();
                }
                else
                {
                    purchaseMethod  = _purchaseMethodRepository.Add(
                    new PurchaseMethod()
                    {
                        Name = request.Description,                        
                    });
                }

                               
                _bookPurchaseMethodRepository.Add(new BookPurchaseMethod()
                {
                    BookId = bookEntity.Id,
                    PurchaseMethodId = purchaseMethod.Id,                                        
                });

                request.Id = purchaseMethod.Id;

                Result.Data = request;               
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when create a purchase method", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }

            return Task.FromResult(Result);

        }
    }
}
