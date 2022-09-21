
namespace NailsSys.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public int Id { get; set; }   
        public string Descricao { get; set; }
        public string TipoProduto { get; set; }
        public decimal Preco { get; set; }
        public bool Descontinuado { get; set; }
    }
}