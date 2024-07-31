using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Books;
using Basis.Bookstore.Core.Application.UseCases.Books.Find;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.Book.Find
{
    public class ListBookHandler : Handler<ListBookCommand>
    {
        private readonly IBookRepository _repository;
        private readonly ILogger<ListBookHandler> _logger;
        public ListBookHandler(IBookRepository repository, ILogger<ListBookHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public override Task<Result> Handle(ListBookCommand request, CancellationToken cancellationToken)
        {
            var booksResult = new List<BookResult>();

            try
            {
                var books = _repository.GetAll();


                var purchaseMethods = new List<PurchaseMethodResult>();
                var subjects = new List<SubjectResult>();
                var authors = new List<AuthorResult>();


                foreach (var book in books)
                {
                    purchaseMethods = [];
                    subjects = [];
                    authors = [];

                    authors.AddRange(book.BookAuthors.Select(author => new AuthorResult
                    {
                        Id = author.Author.Id,
                        Name = author.Author.Name
                    }));

                    purchaseMethods.AddRange(book.BookPurchaseMethods.Select(purchaseMethod => new PurchaseMethodResult
                    {
                        Id = purchaseMethod.PurchaseMethod.Id,
                        Price = purchaseMethod.Price,
                        BookId = book.Id,
                        Description = purchaseMethod.PurchaseMethod.Name
                    }));

                    subjects.AddRange(book.BookSubjects.Select(subject => new SubjectResult
                    {
                        Id = subject.Subject.Id,
                        Description = subject.Subject.Description
                    }));

                    booksResult.Add(new BookResult()
                    {
                        Id = book.Id,                                                
                        Title = book.Title,
                        Description = book.Description,
                        PublishedYear = book.PublishedYear,
                        Publisher = book.Publisher,                        
                        Edition = book.Edition,                        
                        Authors = authors,
                        Subjects = subjects,
                        PurchaseMethods = purchaseMethods                        
                    });
                }

                Result.Data = booksResult;
            }
            catch (Exception error)
            {

                _logger.LogError(error, "An errors occurs when list all books", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }
           

            return Task.FromResult(Result);
        }
    }
}
