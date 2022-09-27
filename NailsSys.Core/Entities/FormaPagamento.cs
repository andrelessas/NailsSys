using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Entities
{
    public class FormaPagamento:BaseEntity
    {
        public FormaPagamento(string descricao, string aVistaAPrazo)
        {
            Descricao = descricao;
            AVistaAPrazo = aVistaAPrazo;     
            DataCriacao = DateTime.Now;               
        }

        public string Descricao { get; private set; }
        public List<Atendimento> Atendimentos { get; private set; }
        public string AVistaAPrazo { get; private set; }
        public bool Descontinuado { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public void AlterarFormaPagamento(string descricao, string aVistaAPrazo)
        {
            Descricao = descricao;
            AVistaAPrazo = aVistaAPrazo;
        }

        public void DescontinuarFormaPagamento()
        {
            if(!Descontinuado)   
                Descontinuado = true;
        }
    }
}