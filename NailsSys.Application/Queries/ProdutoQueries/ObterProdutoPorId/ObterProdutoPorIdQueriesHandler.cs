using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.ProdutoQueries.ObterProdutoPorId
{
    public class ObterProdutoPorIdQueriesHandler : IRequestHandler<ObterProdutoPorIdQueries, ProdutoViewModel>
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ObterProdutoPorIdQueriesHandler(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }
        public async Task<ProdutoViewModel> Handle(ObterProdutoPorIdQueries request, CancellationToken cancellationToken)
        {
            var produto = await _produtoRepository.ObterPorIDAsync(request.Id);

            if (produto == null)
                throw new ExcecoesPersonalizadas("Produto não encontrado.");

            return _mapper.Map<ProdutoViewModel>(produto);
        }
    }
}