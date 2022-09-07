using AutoMapper;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;

namespace NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios
{
    public class ObterUsuariosQueriesHandler : IRequestHandler<ObterUsuariosQueries, IEnumerable<UsuarioViewModel>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public ObterUsuariosQueriesHandler(IUsuarioRepository usuarioRepository,
                                            IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UsuarioViewModel>> Handle(ObterUsuariosQueries request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<UsuarioViewModel>>(await _usuarioRepository.ObterTodosAsync());
        }
    }
}