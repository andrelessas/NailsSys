using Microsoft.EntityFrameworkCore.Storage;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.Infrastructure.Context;
using NailsSys.Infrastructure.Persistense.Repositories;

namespace NailsSys.Infrastructure.Persistense
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly NailsSysContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWorks(NailsSysContext context)
        {
            _context = context;
            ItemAgendamento = new ItemAgendamentoRepository(_context);
            Agendamento = new AgendamentoRepository(_context);
            ItemAtendimento = new ItemAtendimentoRepository(_context);
            Atendimento = new AtendimentoRepository(_context);
            Produto = new ProdutoRepository(_context);
            Cliente = new ClienteRepository(_context);
            Usuario = new UsuarioRepository(_context);
            FormaPagamento = new FormaPagamentoRepository(_context);
        }

        public IItemAgendamentoRepository ItemAgendamento { get; }
        public IAgendamentoRepository Agendamento { get; }
        public IItemAtendimentoRepository ItemAtendimento { get; }
        public IAtendimentoRepository Atendimento { get; }
        public IProdutoRepository Produto { get; }
        public IClienteRepository Cliente { get; }
        public IUsuarioRepository Usuario { get; }
        public IFormaPagamentoRepository FormaPagamento { get; }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (ExcecoesPersonalizadas)
            {
                await _transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}