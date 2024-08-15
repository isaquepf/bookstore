using Basis.Bookstore.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Basis.Bookstore.Api.Model
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo é obrigatório")]
        [Display(Name = "Titulo")]
        [DataType(DataType.Text)]
        [MaxLength(40, ErrorMessage = "O campo Titulo pode ter até 40 caracteres.")]        
        public  string Title { get; set; }

        [Display(Name = "Descrição")]
        [MaxLength(40, ErrorMessage = "O campo Descrição pode ter até 40 caracteres.")]
        public  string Description { get; set; }

        [Display(Name = "Publicado por")]
        [MaxLength(40, ErrorMessage = "O campo Publicado por pode ter até 40 caracteres.")]
        public string Publisher { get; set; }

        [Display(Name = "Edição")]
        public int Edition { get; set; }

        [Required(ErrorMessage = "Data de Publicação é obrigatória")]
        [Display(Name = "Data de Publicação")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public  string PublishedYear { get; set; }

        [Required(ErrorMessage = "Selecione um autor")]
        [Display(Name = "Autores")]        
        public List<string> AuthorIds { get; set; }
        public AuthorViewModel AuthorVM { get; set; }
        public SubjectViewModel SubjectVM { get; set; }

        [Required(ErrorMessage = "Selecione um assunto")]
        [Display(Name = "Assuntos")]        
        public List<string> SubjectIds { get; set; }

        public PurchaseMethodViewModel PurchaseMethodsVM { get; set; }

        public List<PurchaseMethodViewItemModel> PurchaseItems { get; set; }        
    }
}
