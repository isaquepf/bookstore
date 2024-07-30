using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;

namespace Basis.Bookstore.Core.Application.UseCases.Books.Delete
{
    public class DeleteBookHandler : Handler<DeleteBookCommand>
    {
        private readonly IBookRepository _repository;
        public DeleteBookHandler(IBookRepository repository)
        {
            _repository = repository;
        }


        public override Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = _repository.GetById(request.Id);

                if (book == null)
                {
                    Result.AddNotification("O livro não foi encontrado.",  ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                _repository.Remove(book);
            }
            catch (Exception)
            {
                Result.AddNotification("Somenting went wrong", ErrorCode.InternalError);
            }

            return Task.FromResult(Result);
        }
    }
}
