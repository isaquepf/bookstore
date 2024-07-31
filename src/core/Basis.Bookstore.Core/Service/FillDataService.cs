using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Subjects;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;

namespace Basis.Bookstore.Core.Service
{
    public class FillDataService : IFillDataService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        public FillDataService(IAuthorRepository authorRepository, ISubjectRepository subjectRepository)
        {
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
        }

        public List<AuthorResult> GetAllAuthors()
        {
            return _authorRepository.GetAll().ToList().ConvertAll(p => new AuthorResult { Id = p.Id, Name = p.Name });
        }

        public List<SubjectResult> GetAllSubjects()
        {
            return _subjectRepository.GetAll().ToList().ConvertAll(p => new SubjectResult { Id = p.Id, Description = p.Description });
        }
    }
}
