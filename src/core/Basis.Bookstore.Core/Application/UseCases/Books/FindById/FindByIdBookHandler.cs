using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Books.FindById;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.Book.FindById
{
    public class FindByIdBookHandler : Handler<FindByIdBookCommand>
    {      
        private readonly IBookRepository _repo;
        private readonly ILogger<FindByIdBookHandler> _logger;
        public FindByIdBookHandler(IBookRepository repo, ILogger<FindByIdBookHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public override Task<Result> Handle(FindByIdBookCommand request, CancellationToken cancellationToken)
        {

            try
            {
                Result.Data = _repo.GetById(request.Id);              
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when delete author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }
            return Task.FromResult(Result);

        }
    }
}
