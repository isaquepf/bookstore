using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Authors.Update;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.Author.Create
{
    public class UpdateAuthorHandler : Handler<UpdateAuthorCommand>
    {       
        private readonly IAuthorRepository _repository;
        private readonly ILogger<UpdateAuthorHandler> _logger;
        public UpdateAuthorHandler(IAuthorRepository repository, ILogger<UpdateAuthorHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var author = _repository.GetById(request.Id);

                if (author == null)
                {
                    Result.AddNotification("Não foi encontrado um autor com esse id.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }


                author.Name = request.Author.Name;
                _repository.Update(author);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when update an author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }
           

            return Task.FromResult(Result);
        }
    }
}
