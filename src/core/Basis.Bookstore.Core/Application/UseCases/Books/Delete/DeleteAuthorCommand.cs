using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.Books.Delete
{
    public class DeleteBookCommand : Command<DeleteBookCommand>
    {
        public int Id { get; set; }

        public DeleteBookCommand()
        {

        }
        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }
}
