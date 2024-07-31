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
        private readonly IBookPurchaseMethodRepository _bookPurchaseMethodRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;
        private readonly ILogger<CreateBookHandler> _logger;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IPurchaseMethodRepository _purchaseMethodRepository;

        public CreateBookHandler(
            IBookRepository repo,
            IBookAuthorRepository repoAuthorBook,
            IAuthorRepository repoAuthor,
            ILogger<CreateBookHandler> logger,
            IBookPurchaseMethodRepository bookPurchaseMethodRepository,
            IBookSubjectRepository bookSubjectRepository,
            ISubjectRepository subjectRepository,
            IPurchaseMethodRepository purchaseMethodRepository)
        {
            _bookRepository = repo;
            _bookAuthorRepository = repoAuthorBook;
            _authorRepository = repoAuthor;
            _logger = logger;
            _bookPurchaseMethodRepository = bookPurchaseMethodRepository;
            _bookSubjectRepository = bookSubjectRepository;
            _subjectRepository = subjectRepository;
            _purchaseMethodRepository = purchaseMethodRepository;
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

                var subjects = _subjectRepository.Get(p => request.SubjectsIds.Contains(p.Id)).ToList();

                if (subjects == null || !subjects.Any())
                {
                    Result.AddNotification("Subjects not Found", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }
 


                var book = _bookRepository.Add(new Domain.Entities.Book
                {
                    Title = request.Title,
                    Description = request.Description,
                    Publisher = request.Publisher,
                    Edition = request.Edition,
                    PublishedYear = request.PublishedAt
                });

                var bookResult = new BookResult()
                {
                    Id = book.Id,
                    Description = book.Description,
                    Edition = book.Edition,
                    PublishedYear = book.PublishedYear,
                    Title = request.Title,
                    Publisher = book.Publisher,
                    Authors = authors.Select(p => new AuthorResult { 
                        Id = p.Id,
                        Name  = p.Name,
                    }).ToList(),
                    Subjects = subjects.Select(p => new SubjectResult
                    {
                        Id = p.Id,
                        Description = p.Description
                    }).ToList(),
                    PurchaseMethods = request.PurchaseMethods.Select(p => new PurchaseMethodResult
                    {
                        BookId = book.Id,
                        Description = p.Description,
                        Price = p.Price,
                        Id = p.Id
                    }).ToList(),                    
                };

                foreach (var subjectId in request.SubjectsIds)
                {
                    _bookSubjectRepository.Add(new BookSubject
                    {
                        BookId = book.Id,
                        SubjectId = subjectId
                    });
                }

                foreach (var author in authors)
                {
                     _bookAuthorRepository.Add(new BookAuthor()
                    {
                        AuthorId = author.Id,
                        BookId = book.Id
                    });
                }

                foreach (var purchaseMethod in request.PurchaseMethods)
                {
                    _bookPurchaseMethodRepository.Add(new BookPurchaseMethod()
                    {
                        BookId = book.Id,
                        PurchaseMethodId = purchaseMethod.Id,
                        Price = purchaseMethod.Price,
                    });
                }
               
                Result.Data = bookResult;
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
