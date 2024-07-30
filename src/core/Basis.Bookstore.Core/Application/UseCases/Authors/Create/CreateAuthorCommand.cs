using Basis.Bookstore.Core.Application.Base;
using Flunt.Validations;

namespace Basis.Bookstore.Core.Application.UseCases.Author.Create
{
    public class CreateAuthorCommand : Command<CreateAuthorCommand>, IValidatable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CreateAuthorCommand()
        {

        }

        public CreateAuthorCommand(string name)
        {
            Name = name;
        }

        public void Validate()
        {
            var contract = new Contract();

            contract
                .IsNull(Name, nameof(Name), "Name is required.");
        }
    }
}
