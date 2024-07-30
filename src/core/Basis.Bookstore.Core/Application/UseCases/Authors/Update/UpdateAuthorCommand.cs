

using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Author.Create;

namespace Basis.Bookstore.Core.Application.UseCases.Authors.Update
{
    public class UpdateAuthorCommand : Command<UpdateAuthorCommand>
    {
        public int Id { get; set; }
        public CreateAuthorCommand Author { get; set; }
        public UpdateAuthorCommand()
        {
        }

        public UpdateAuthorCommand(int id, CreateAuthorCommand author)
        {
            Id = id;
            Author = author;
        }

    }
}
