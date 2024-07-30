using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.Subjects.FindById
{
    public class GetByIdSubjectCommand : Command<GetByIdSubjectCommand>
    {
        public int Id { get; set; }


        public GetByIdSubjectCommand()
        {

        }
        public GetByIdSubjectCommand(int id)
        {
            Id = id;
        }

    }
}
