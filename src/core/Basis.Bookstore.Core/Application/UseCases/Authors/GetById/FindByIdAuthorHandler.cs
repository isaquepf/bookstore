using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Author.GetById
{
    public class FindByIdAuthorHandler : Handler<FindByIdAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly ILogger<FindByIdAuthorHandler> _logger;
        public FindByIdAuthorHandler(IAuthorRepository repository, ILogger<FindByIdAuthorHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(FindByIdAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _repository.GetById(request.Id);

                Result.Data = author;
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
