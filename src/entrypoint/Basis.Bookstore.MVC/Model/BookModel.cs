using Basis.Bookstore.Mvc.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

        public List<string> AuthorIds { get; set; }
        public AuthorViewModel AuthorVM { get; set; }
        public SubjectViewModel SubjectVM { get; set; }

        public List<string> SubjectIds { get; set; }

        public PurchaseMethodViewModel PurchaseMethodsVM { get; set; }
        public List<PurchaseMethodViewItemModel> PurchaseItems { get; set; }
        

        public List<string> PurchaseMethodsIds { get; set; }        
    }
}
