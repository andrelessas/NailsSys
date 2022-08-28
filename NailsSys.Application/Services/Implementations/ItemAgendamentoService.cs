using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NailsSys.Application.InputModels;
using NailsSys.Application.Services.Interfaces;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Notificacoes;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Application.Services.Implementations
{
    public class ItemAgendamentoService : IItemAgendamentoService
    {
        private readonly NailsSysContext _context;

        public ItemAgendamentoService(NailsSysContext context)
        {
            _context = context;
        }
        public void AlterarItem(AlterarItemAgendamentoInputModel inpuModel)
        {
            var item = _context.ItemAgendamento.SingleOrDefault(x=>x.Id == inpuModel.Id);
            item.AlterarQuantidade(inpuModel.Quantidade);
            _context.SaveChanges();
        }

        public void InserirItem(NovoItemAgendamentoInputModel inpuModel)
        {
            var produto = _context.Produto.SingleOrDefault(p => p.Id == inpuModel.IdProduto);
            _context.ItemAgendamento.Add(new ItemAgendamento(inpuModel.IdProduto,inpuModel.IdAgendamento,inpuModel.Quantidade,produto.Preco));
            _context.SaveChanges();
        }

        public List<ItemAgendamentoViewModel> ObterItens(int idAgendamento)
        {
            var itens = _context.ItemAgendamento.Include(p => p.Produto).Where(x=>x.IdAgendamento == idAgendamento);
            // var produto = _context.Produto;


            // return new ItemAgendamentoViewModel
            //                             (                                            
            //                                 itens.
            //                                 i.Quantidade,
            //                                 i.PrecoInicial,
            //                                 i.Id
            //                             );

            return null; 
        }

        public void RemoverItem(int id)
        {
            var item = _context.ItemAgendamento.SingleOrDefault(x=>x.Id == id);
            if(item == null)
                throw new ExcecoesPersonalizadas("Item não encontrado");

            _context.ItemAgendamento.Remove(item);
            _context.SaveChanges();
        }
    }
}