using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Subjects.FindById
{
    public class GetByIdSubjectHandler : Handler<GetByIdSubjectCommand>
    {

        private readonly ISubjectRepository _repository;
        private readonly ILogger<GetByIdSubjectHandler> _logger;
        public GetByIdSubjectHandler(ISubjectRepository repository, ILogger<GetByIdSubjectHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public override Task<Result> Handle(GetByIdSubjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
               var subject = _repository.GetById(request.Id);

                Result.Data = new SubjectResult
                {
                    Description = subject.Description,
                    Id = subject.Id,
                };
            }
            catch (Exception error)
            {
                _logger.LogError(error, "An errors occurs when find a subject", GetFormattedException(error));
                Result.AddNotification("Ocorreu um problema tente novamente em alguns instantes.", ErrorCode.InternalError);
            }


            return Task.FromResult(Result);
        }
    }
}
