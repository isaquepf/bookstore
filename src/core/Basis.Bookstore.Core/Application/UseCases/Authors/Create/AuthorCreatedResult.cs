namespace Basis.Bookstore.Core.Application.UseCases.Author.Create
{
    public class AuthorCreatedResult
    {
        public AuthorCreatedResult(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
