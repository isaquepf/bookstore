

using Basis.Bookstore.Core.Application.Base;
using Basis.Bookstore.Core.Application.UseCases.PurchaseMethods.Create;

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
        public List<int> SubjectsIds { get; set; }

        public List<CreatePurchaseMethodCommand> PurchaseMethods { get; set; }

        public CreateBookCommand()
        {
        }

        public CreateBookCommand(string title, string publisher, int edition, string publishedAt, List<int> authorsIds, string description, List<int> subjectsIds)
        {
            Title = title;
            Publisher = publisher;
            Edition = edition;
            PublishedAt = publishedAt;
            AuthorsIds = authorsIds;
            Description = description;
            SubjectsIds = subjectsIds;
        }
    }
}
