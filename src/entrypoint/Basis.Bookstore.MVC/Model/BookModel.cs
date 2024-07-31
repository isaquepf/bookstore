using Microsoft.AspNetCore.Mvc.Rendering;

namespace Basis.Bookstore.Api.Model
{
    public class BookModel
    {
        public int Id { get; set; }
        public  string Title { get; set; }
        public  string Description { get; set; }
        public  string Publisher { get; set; }
        public  int Edition { get; set; }
        public  string PublishedYear { get; set; }
        public List<SelectListItem> AuthorsIds { get; set; }
        public List<SelectListItem> SubjectsIds { get; set; }
        public List<PurchaseMethodModel> PurchaseMethods { get; set; }
    }
}
