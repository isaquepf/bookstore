namespace Basis.Bookstore.Core.Domain.Contracts
{
    public class BookDto
    {
        public int Codl { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Assunto { get; set; }

        public string FormaCobranca { get; set; }

        public DateTime DataPublicacao { get; set; }

        public decimal Preco { get; set; }
    }
}
