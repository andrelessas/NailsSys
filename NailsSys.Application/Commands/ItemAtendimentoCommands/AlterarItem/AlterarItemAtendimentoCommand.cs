using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace NailsSys.Application.Commands.ItemAtendimentoCommands.AlterarItem
{
    public class AlterarItemAtendimentoCommand:IRequest<Unit>
    {
        public AlterarItemAtendimentoCommand(int idAtendimento, int item, int idProduto, int quantidade)
        {
            IdAtendimento = idAtendimento;
            Item = item;
            IdProduto = idProduto;
            Quantidade = quantidade;
        }

        public int IdAtendimento { get; set; }
        public int Item { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }
}