using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Basis.Bookstore.Core.Application.UseCases.Author.Create
{
    public class CreateAuthorHandler : Handler<CreateAuthorCommand>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<CreateAuthorHandler> _logger;
        public CreateAuthorHandler(IAuthorRepository repo, ILogger<CreateAuthorHandler> logger)
        {
            _authorRepository = repo;
            _logger = logger;
        }

        public override async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try {

                request.Validate();

                if (request.Invalid)
                {
                    Result.AddNotifications(request.Notifications, ErrorCode.UnprocessableEntity);
                    return await Task.FromResult(Result);
                }
               
                var authors = _authorRepository.Get(p => p.Name == request.Name);

                if (authors != null && authors.Any())
                {
                    Result.AddNotification("Já existe um autor cadastrado com esse nome.");
                    return await Task.FromResult(Result);
                }

                var author = _authorRepository.Add(new Domain.Entities.Author() { Name = request.Name });


                Result.Data = new AuthorCreatedResult(
                    author.Id
                    );
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when delete author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }



            return await Task.FromResult(Result);
        }
    }
}
