using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods;
using Basis.Bookstore.Core.Application.UseCases.Subjects;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;

namespace Basis.Bookstore.Core.Service
{
    public class FillDataService : IFillDataService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IPurchaseMethodRepository _purchaseMethodRepository;
        public FillDataService(IAuthorRepository authorRepository, ISubjectRepository subjectRepository, IPurchaseMethodRepository purchaseMethodRepository)
        {
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
            _purchaseMethodRepository = purchaseMethodRepository;
        }

        public List<AuthorResult> GetAllAuthors()
        {
            return _authorRepository.GetAll().ToList().ConvertAll(p => new AuthorResult { Id = p.Id, Name = p.Name });
        }

        public List<PurchaseMethodResult> GetAllPurchaseMethods()
        {
            return _purchaseMethodRepository.GetAll().ToList().ConvertAll(p => new PurchaseMethodResult { Id = p.Id, Name = p.Name });

        }

        public List<SubjectResult> GetAllSubjects()
        {
            return _subjectRepository.GetAll().ToList().ConvertAll(p => new SubjectResult { Id = p.Id, Description = p.Description });
        }
    }
}
