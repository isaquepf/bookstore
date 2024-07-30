using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.Books.FindById
{
    public class FindByIdBookCommand : Command<FindByIdBookCommand>
    {
        public int Id { get; set; }
        public FindByIdBookCommand()
        {

        }

        public FindByIdBookCommand(int id)
        {
            Id = id;
        }

    }
}
