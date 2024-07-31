using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Subject.Update;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Subjects.Update
{
    public class UpdateSubjectHandler : Handler<UpdateSubjectCommand>
    {

        private readonly ISubjectRepository _repository;
        private readonly ILogger<UpdateSubjectHandler> _logger;
        public UpdateSubjectHandler(ISubjectRepository repository, ILogger<UpdateSubjectHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var subject = _repository.GetById(request.Id);

                if (subject == null)
                {
                    Result.AddNotification("Não foi encontrado um autor com esse id.", ErrorCode.NotFound);
                    return Task.FromResult(Result);
                }

                subject.Description = request.Subject.Description;

                _repository.Update(subject);

                Result.Data = new SubjectResult
                {
                    Description = subject.Description,
                    Id = subject.Id,
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when create a purchase method", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }

            return Task.FromResult(Result);
        }
    }
}
