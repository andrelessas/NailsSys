using Microsoft.EntityFrameworkCore;
using NailsSys.Application.InputModels;
using NailsSys.Application.Services.Interfaces;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Notificacoes;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Application.Services.Implementations
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly NailsSysContext _context;

        public AgendamentoService(NailsSysContext context)
        {
            _context = context;
        }
        public void AlterarAgendamento(AlterarAgendamentoInputModel inputModel)
        {
            var agendamento = _context.Agendamento.FirstOrDefault(a => a.Id == inputModel.Id);
            agendamento.AlterarAgendamento(inputModel.Cliente.Id,inputModel.DataAtendimento,inputModel.InicioPrevisto,inputModel.TerminoPrevisto);
            _context.Agendamento.Update(agendamento);
            _context.SaveChanges();
        }

        public void CancelarAgendamento(int id)
        {
            var agendamento = _context.Agendamento.SingleOrDefault(x=>x.Id == id);
            agendamento.CancelarAgendamento();
            _context.Agendamento.Update(agendamento);
            _context.SaveChanges();
        }

        public List<AgendamentoViewModel> ObterUnhasAgendadasHoje()
        {
            var agendamento = _context.Agendamento.Where(x=>x.DataAtendimento.Date == DateTime.Now.Date); 
            var Cliente = _context.Cliente;
            var agendamentoList = agendamento.Select(x => new AgendamentoViewModel
                                                (
                                                    x.DataAtendimento,
                                                    x.InicioPrevisto,
                                                    x.TerminoPrevisto,
                                                    x.Cliente.NomeCliente
                                                )).ToList();
            return agendamentoList;
        }

        public List<AgendamentoViewModel> ObterUnhasAgendadasPorData(DateTime data)
        {
            var agendamentos = _context.Agendamento.Include(i => i.ItemAgendamentos)
                                                   .Include(c => c.Cliente) 
                                                   .Where(x=>x.DataAtendimento == data); 
            var cliente = _context.Cliente;

            return agendamentos.Select(x => new AgendamentoViewModel
                                                (                                                    
                                                    x.DataAtendimento,
                                                    x.InicioPrevisto,
                                                    x.TerminoPrevisto,
                                                    x.Cliente.NomeCliente
                                                )).ToList();
        }

        public List<AgendamentoViewModel> ObterUnhasAgendadasPorPeriodoDoDia(DateTime horarioInicial, DateTime horarioFinal)
        {
            var Agendamento = _context.Agendamento.Where(x=>x.InicioPrevisto <= horarioInicial && x.InicioPrevisto >= horarioFinal); 
            var Cliente = _context.Cliente;           
            return Agendamento.Select(x => new AgendamentoViewModel
                                                (                                                    
                                                    x.DataAtendimento,
                                                    x.InicioPrevisto,
                                                    x.TerminoPrevisto,
                                                    x.Cliente.NomeCliente
                                                )).ToList();
        }

        public void NovoAgendamento(NovoAgendamentoInputModel inputModel)
        {
            var cliente = _context.Cliente.SingleOrDefault(x=>x.Id == inputModel.idCliente && x.Bloqueado == false);
            // var itens = inputModel.Itens;

            if(cliente == null)
                throw new ExcecoesPersonalizadas("Nenhum cliente encontrado.");

            _context.Agendamento.Add(
                new Agendamento(inputModel.idCliente,inputModel.DataAtendimento,inputModel.InicioPrevisto,inputModel.TerminoPrevisto)
            );

            // foreach (var item in itens)
            // {
            //     var produto = _context.Produto.FirstOrDefault(x => x.Id == item.IdProduto);
            //     _context.ItemAgendamento.Add(new ItemAgendamento(item.IdProduto,inputModel.,item.Quantidade,item.PrecoInicial));
            // }

            _context.SaveChanges();
        }
    }
}