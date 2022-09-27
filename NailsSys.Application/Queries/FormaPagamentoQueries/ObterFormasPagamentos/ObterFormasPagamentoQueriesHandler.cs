using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormasPagamentos
{
    public class ObterFormasPagamentoQueriesHandler : IRequestHandler<ObterFormasPagamentoQueries,PaginationResult<FormaPagamentoViewModel>>
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IMapper _mapper;

        public ObterFormasPagamentoQueriesHandler(IFormaPagamentoRepository formaPagamentoRepository,
                                                    IMapper mapper)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResult<FormaPagamentoViewModel>> Handle(ObterFormasPagamentoQueries request, CancellationToken cancellationToken)
        {
            var formaPagamentos = await _formaPagamentoRepository.ObterTodosAsync(request.Page);
            if(formaPagamentos == null || formaPagamentos.Data.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhuma forma de pagamento encontrada.");

            return _mapper.Map<PaginationResult<FormaPagamentoViewModel>>(formaPagamentos);
        }
    }
}