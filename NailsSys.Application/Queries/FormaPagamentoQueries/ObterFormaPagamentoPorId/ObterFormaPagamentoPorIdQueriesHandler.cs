using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormaPagamentoPorId
{
    public class ObterFormaPagamentoPorIdQueriesHandler : IRequestHandler<ObterFormaPagamentoPorIdQueries, FormaPagamentoViewModel>
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IMapper _mapper;

        public ObterFormaPagamentoPorIdQueriesHandler(IFormaPagamentoRepository formaPagamentoRepository,
                                                        IMapper mapper)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
            _mapper = mapper;
        }
        public async Task<FormaPagamentoViewModel> Handle(ObterFormaPagamentoPorIdQueries request, CancellationToken cancellationToken)
        {
            var formaPagamento = await _formaPagamentoRepository.ObterPorIDAsync(request.Id);

            if(formaPagamento == null)
                throw new ExcecoesPersonalizadas("Forma de pagamento não encontrada.");

            return _mapper.Map<FormaPagamentoViewModel>(formaPagamento);
        }
    }
}