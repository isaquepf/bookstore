using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Subjects;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Basis.Bookstore.Core.Application.UseCases.Subject.Find
{
    public class ListSubjectHandler : Handler<ListSubjectCommand>
    {
        private readonly ILogger<ListSubjectHandler> _logger;
        private readonly ISubjectRepository _repo;
        public ListSubjectHandler(ISubjectRepository repo, ILogger<ListSubjectHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public override Task<Result> Handle(ListSubjectCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var subjects =  _repo.GetAll();

                var subjectsModel = new List<SubjectResult>();

                foreach (var subject in subjects)
                {
                    subjectsModel.Add(new SubjectResult
                    {
                        Description = subject.Description,
                        Id = subject.Id,
                    });
                }

                Result.Data = subjectsModel;
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
