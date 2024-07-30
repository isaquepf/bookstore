using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Subject.Create;

namespace Basis.Bookstore.Core.Application.UseCases.Subject.Update
{
    public class UpdateSubjectCommand : Command<UpdateSubjectCommand>
    {
        public int Id { get; set; }
        public CreateSubjectCommand Subject { get; set; }


        public UpdateSubjectCommand()
        {
        }
        public UpdateSubjectCommand(int id, CreateSubjectCommand author)
        {
            Id = id;
            Subject = author;
        }

    }
}
