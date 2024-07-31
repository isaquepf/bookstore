using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Subjects;

namespace Basis.Bookstore.Core.Service
{
    public interface IFillDataService
    {
        List<AuthorResult> GetAllAuthors();

        List<SubjectResult> GetAllSubjects();

    }
}
