
namespace NailsSys.Core.Entities
{
    public class Produto:BaseEntity
    {
        public Produto(string descricao, string tipoProduto, decimal preco)
        {
            Descricao = descricao;
            TipoProduto = tipoProduto;
            Preco = preco;
            DataCriacao = DateTime.Now;
        }

        public string Descricao { get; private set; }
        public string TipoProduto { get; private set; }
        public decimal Preco { get; private set; }
        public bool Descontinuado { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public List<ItemAgendamento> ItemAgendamentos { get; private set; }
        public List<ItemAtendimento> ItemAtendimentos { get; private set; }

        public void DescontinuarProduto()
        {
            if(!Descontinuado)
                Descontinuado = true;
        }

        public void AlterarProduto(string descricao, string tipoProduto, decimal preco)
        {
            Descricao = descricao;
            TipoProduto = tipoProduto;
            Preco = preco;
        }
    }
}