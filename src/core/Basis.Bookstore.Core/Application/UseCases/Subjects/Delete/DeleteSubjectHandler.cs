using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Subjects;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MyBook.Application.UseCases.Subject.Delete
{
    public class DeleteSubjectHandler : Handler<DeleteSubjectCommand>
    {
        private readonly ISubjectRepository _repository;
        private readonly ILogger<DeleteSubjectHandler> _logger;
        public DeleteSubjectHandler(ISubjectRepository repository, ILogger<DeleteSubjectHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public override Task<Result> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = _repository.GetById(request.Id);

                if (subject == null)
                {
                    Result.AddNotification("Não foi encontrado um assunto válido.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                _repository.Remove(subject);

                Result.Data = new SubjectResult
                {
                    Description = subject.Description,
                    Id = subject.Id,
                };
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
