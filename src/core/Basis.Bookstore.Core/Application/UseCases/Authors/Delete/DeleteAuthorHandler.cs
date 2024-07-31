using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Author.Delete
{
    public class DeleteAuthorHandler : Handler<DeleteAuthorCommand>
    {
        private readonly IAuthorRepository _repository;
        private readonly ILogger<DeleteAuthorHandler> _logger;
        public DeleteAuthorHandler(IAuthorRepository repository, ILogger<DeleteAuthorHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _repository.GetById(request.Id);

                if (author == null)
                {
                    Result.AddNotification("Não foi encontrado um autor com esse nome.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                _repository.Remove(author);

                Result.Data = new AuthorResult
                {
                   Id = author.Id,
                   Name = author.Name,
                };
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
