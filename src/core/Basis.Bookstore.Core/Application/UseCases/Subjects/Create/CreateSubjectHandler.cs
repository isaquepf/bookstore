using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Subject.Create;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Subjects.Create
{
    public class CreateSubjectHandler : Handler<CreateSubjectCommand>
    {
        private readonly ISubjectRepository _repository;
        
        private readonly ILogger<CreateSubjectHandler> _logger;
        public CreateSubjectHandler(ISubjectRepository repository, ILogger<CreateSubjectHandler> logger)
        {
            _repository = repository;            
            _logger = logger;
        }

        public override Task<Result> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
        {

            try
            {               
                var subject = _repository.Get(p => p.Description == request.Description).FirstOrDefault();

                subject ??= _repository.Add(new Domain.Entities.Subject() { Description = request.Description });

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
