using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace MyBook.Application.UseCases.Book.Update
{
    public class UpdateBookHandler : Handler<UpdateBookCommand>
    {
        private readonly IBookRepository _repository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;
        private readonly IBookPurchaseMethodRepository _bookPurchaseMethod;
        private readonly ILogger<UpdateBookHandler> _logger;

        public UpdateBookHandler(IBookRepository repository,
            IBookAuthorRepository bookAuthorRepository,
            IBookSubjectRepository bookSubjectRepository,
            IBookPurchaseMethodRepository bookPurchaseMethod,
            ILogger<UpdateBookHandler> logger)
        {
            _repository = repository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookSubjectRepository = bookSubjectRepository;
            _bookPurchaseMethod = bookPurchaseMethod;
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

                RefreshRelationShips(request, book);

                _repository.Update(book);
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when delete author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }
            return Task.FromResult(Result);
        }

        private void RefreshRelationShips(UpdateBookCommand request, Basis.Bookstore.Core.Domain.Entities.Book book)
        {
            _bookAuthorRepository.RemoveRange(book.BookAuthors);
            _bookSubjectRepository.RemoveRange(book.BookSubjects);
            _bookPurchaseMethod.RemoveRange(book.BookPurchaseMethods);

            _bookAuthorRepository.AddRange(
                request.Book.AuthorsIds
                .ConvertAll(authorId => new BookAuthor(book.Id, authorId))
            );

            _bookSubjectRepository.AddRange(
                request.Book.SubjectsIds
                .ConvertAll(subjectId => new BookSubject(book.Id, subjectId))
             );

            _bookPurchaseMethod.AddRange(request.Book.PurchaseMethods.ConvertAll(p => new BookPurchaseMethod
            {
                BookId = book.Id,
                PurchaseMethodId = p.Id,
                Price = p.Price,
            }));
        }
    }
}
