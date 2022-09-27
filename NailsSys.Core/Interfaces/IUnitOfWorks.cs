using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.Interfaces;

namespace NailsSys.Core.Interfaces
{
    public interface IUnitOfWorks:IDisposable
    {
        public IItemAgendamentoRepository ItemAgendamento { get; }
        public IAgendamentoRepository Agendamento { get; }
        public IItemAtendimentoRepository ItemAtendimento { get; }
        public IAtendimentoRepository Atendimento { get; }
        public IProdutoRepository Produto { get; }
        public IClienteRepository Cliente { get; }
        public IUsuarioRepository Usuario { get; }
        public IFormaPagamentoRepository FormaPagamento { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
    }
}