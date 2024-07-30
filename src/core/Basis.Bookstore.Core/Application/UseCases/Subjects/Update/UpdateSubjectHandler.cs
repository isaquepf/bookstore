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
                var entity = _repository.GetById(request.Id);
                entity.Description = request.Subject.Description;
                _repository.Update(entity);

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
