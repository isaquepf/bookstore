using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Author.Find
{
    public class ListAuthorHandler : Handler<ListAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly ILogger<ListAuthorHandler> _logger;
        public ListAuthorHandler(IAuthorRepository repository, ILogger<ListAuthorHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public override Task<Result> Handle(ListAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = _repository.GetAll();

                authors ??= [];

                Result.Data = authors;
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
