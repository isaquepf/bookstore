using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Books;
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
                var book = _repo.GetById(request.Id);

                Result.Data = new BookResult
                {
                    Id = book.Id,
                    Description = book.Description,
                    Edition = book.Edition,
                    PublishedYear = book.PublishedYear,
                    Publisher = book.Publisher,
                    Title = book.Title,
                    Authors = book.BookAuthors != null ? book.BookAuthors.Select(p => new AuthorResult
                    {
                        Id = p.Author.Id,
                        Name = p.Author.Name
                    }).ToList() : [],
                    Subjects = book.BookSubjects != null ? book.BookSubjects.Select(p => new SubjectResult
                    {
                        Id = p.Subject.Id,
                        Description = p.Subject.Description,
                        BookId = p.BookId,
                    }).ToList() : [],
                    PurchaseMethods = book.BookPurchaseMethods != null ?  book.BookPurchaseMethods.Select(p => new PurchaseMethodResult
                    {
                        Id = p.PurchaseMethod.Id,
                        BookId = p.BookId,
                        Description = p.PurchaseMethod.Name,
                        Price = p.Price,
                    }).ToList() : []
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when find a book", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }
            return Task.FromResult(Result);

        }
    }
}
