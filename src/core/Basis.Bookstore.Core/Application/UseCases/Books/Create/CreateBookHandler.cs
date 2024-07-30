using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Book.Create;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Books.Create
{
    public class CreateBookHandler : Handler<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly ILogger<CreateBookHandler> _logger;
        public CreateBookHandler(IBookRepository repo, IBookAuthorRepository repoAuthorBook, IAuthorRepository repoAuthor, ILogger<CreateBookHandler> logger)
        {
            _bookRepository = repo;
            _bookAuthorRepository = repoAuthorBook;
            _authorRepository = repoAuthor;
            _logger = logger;
        }

        public override Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = _authorRepository.Get(p => request.AuthorsIds.Contains(p.Id)).ToList();

                if (authors == null || !authors.Any())
                {
                    Result.AddNotification("Author not Found", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                //Create Book
                var bookEntity = _bookRepository.Add(new Domain.Entities.Book
                {
                    Title = request.Title,
                    Description = request.Description,
                    Publisher = request.Publisher,
                    Edition = request.Edition,
                    PublishedYear = request.PublishedAt
                });

                foreach (var author in authors)
                {
                    _bookAuthorRepository.Add(new BookAuthor()
                    {
                        AuthorId = author.Id,
                        BookId = bookEntity.Id
                    });
                }


                request.Id = bookEntity.Id;

                Result.Data = request;
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when create a book", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }

            return Task.FromResult(Result);
        }
    }
}
