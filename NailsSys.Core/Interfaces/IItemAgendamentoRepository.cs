using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NailsSys.Core.DTOs;
using NailsSys.Core.Entities;
using NailsSys.Core.Models;

namespace NailsSys.Core.Interfaces
{
    public interface IItemAgendamentoRepository : IRepository<ItemAgendamento>
    {
        Task<PaginationResult<ItemAgendamentoDTO>> ObterItensAsync(int idAgendamento, int page);
        Task<ItemAgendamentoDTO> ObterItemPorId(int idAgendamento);
        Task<int> ObterMaxItem(int idAgendamento);
        Task InserirItemAsync(ItemAgendamento itemAgendamento);
    }
}