using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios
{
    public class ObterUsuariosQueriesHandler : IRequestHandler<ObterUsuariosQueries, PaginationResult<UsuarioViewModel>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ObterUsuariosQueriesHandler(IUsuarioRepository usuarioRepository,
                                            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<PaginationResult<UsuarioViewModel>> Handle(ObterUsuariosQueries request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObterTodosAsync(request.Page);

            if(usuarios == null || usuarios.Data.Count() == 0)
                throw new ExcecoesPersonalizadas("Nenhum usuário encontrado.");

            return _mapper.Map<PaginationResult<UsuarioViewModel>>(usuarios);
        }
    }
}