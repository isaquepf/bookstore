using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.Book.Create;

namespace MyBook.Application.UseCases.Book.Update
{
    public class UpdateBookCommand : Command<UpdateBookCommand>
    {
        public int Id { get; set; }
        public CreateBookCommand Book { get; set; }


        public UpdateBookCommand()
        {
        }
        public UpdateBookCommand(int id, CreateBookCommand book)
        {
            Id = id;
            Book = book;
        }

    }
}
