using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.Book.Update
{
    public class UpdateBookHandler : Handler<UpdateBookCommand>
    {       
        private readonly IBookRepository _repository;
        private readonly ILogger<UpdateBookHandler> _logger;
        public UpdateBookHandler(IBookRepository repository, ILogger<UpdateBookHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = _repository.GetById(request.Id);
                book.Title = request.Book.Title;
                book.Publisher = request.Book.Publisher;
                book.Edition = request.Book.Edition;
                book.Publisher = request.Book.Publisher;
                _repository.Update(book);
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
