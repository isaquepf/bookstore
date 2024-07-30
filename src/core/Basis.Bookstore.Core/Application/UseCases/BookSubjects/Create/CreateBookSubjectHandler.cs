using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.BookSubjects.Create
{
    public class CreateBookSubjectHandler : Handler<CreateBookSubjectCommand>
    {
        private readonly ISubjectRepository _repository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;
        private readonly ILogger<CreateBookSubjectHandler> _logger;
        public CreateBookSubjectHandler(ISubjectRepository repository, IBookRepository bookRepository, IBookSubjectRepository bookSubjectRepository, ILogger<CreateBookSubjectHandler> logger)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _bookSubjectRepository = bookSubjectRepository;
            _logger = logger;
        }

        public override Task<Result> Handle(CreateBookSubjectCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var book = _bookRepository.GetById(request.BookId);

                if (book == null)
                {
                    Result.AddNotification("O livro não foi encontrado.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }
                
                
                var subject = _repository.Get(p => p.Description == request.Description).FirstOrDefault();

                subject ??= _repository.Add(new Domain.Entities.Subject() { Description = request.Description });
                
                _bookSubjectRepository.Add(new BookSubject()
                {
                    BookId = book.Id,
                    SubjectId = subject.Id
                });

                request.Id = subject.Id;

                Result.Data = request;

            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when find an author", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);

            }


            return Task.FromResult(Result);
        }
    }
}
