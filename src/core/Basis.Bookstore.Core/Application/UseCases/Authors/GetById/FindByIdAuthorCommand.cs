using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.Authors.GetById
{
    public class FindByIdAuthorCommand : Command<FindByIdAuthorCommand>
    {
        public int Id { get; set; }


        public FindByIdAuthorCommand()
        {

        }
        public FindByIdAuthorCommand(int id)
        {
            Id = id;
        }

    }
}
