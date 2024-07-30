using Basis.Bookstore.Core.Application.Base;
using Flunt.Validations;

namespace Basis.Bookstore.Core.Application.UseCases.BookSubjects.Create
{
    public class CreateBookSubjectCommand : Command<CreateBookSubjectCommand>, IValidatable
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int BookId { get; set; }
        public CreateBookSubjectCommand()
        {

        }

        public CreateBookSubjectCommand(string description, int bookId)
        {
            Description = description;
            BookId = bookId;
        }

        public void Validate()
        {
            var contract = new Contract();
            contract
                .IsNull(Description, nameof(Description), "Description field is required.");
        }
    }
}
