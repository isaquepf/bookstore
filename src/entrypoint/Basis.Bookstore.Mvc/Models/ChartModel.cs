
using System.ComponentModel.DataAnnotations;

namespace Basis.Bookstore.Mvc.Models
{
    public class ChartModel
    {
        public int Codl { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Assunto { get; set; }
        public string FormaCobranca { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:dd/MM/yyyy}")]

        public DateTime DataPublicacao { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal Preco { get; set; }
    }
}
