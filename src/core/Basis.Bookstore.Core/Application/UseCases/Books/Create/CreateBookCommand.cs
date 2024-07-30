

using Basis.Bookstore.Core.Application.Base;

namespace Basis.Bookstore.Core.Application.UseCases.Book.Create
{
    public class CreateBookCommand : Command<CreateBookCommand>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int Edition { get; set; }
        public string PublishedAt { get; set; }
        public List<int> AuthorsIds { get; set; }
        public CreateBookCommand()
        {
        }

        public CreateBookCommand(string title, string publisher, int edition, string publishedAt, List<int> AuthorsIds, string description)
        {
            Title = title;
            Publisher = publisher;
            Edition = edition;
            PublishedAt = publishedAt;
            AuthorsIds = AuthorsIds;
            Description = description;
        }
    }
}
