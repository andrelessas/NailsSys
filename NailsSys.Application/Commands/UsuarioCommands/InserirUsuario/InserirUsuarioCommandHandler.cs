using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Core.Entities;
using NailsSys.Core.Enums;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Services;

namespace NailsSys.Application.Commands.UsuarioCommands.InserirUsuario
{
    public class InserirUsuarioCommandHandler : IRequestHandler<InserirUsuarioCommand, Unit>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAutenticacaoService _autenticacaoService;

        public InserirUsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                            IAutenticacaoService autenticacaoService)
        {
            _usuarioRepository = usuarioRepository;
            _autenticacaoService = autenticacaoService;
        }
        public async Task<Unit> Handle(InserirUsuarioCommand request, CancellationToken cancellationToken)
        {
            var hashSenha = _autenticacaoService.ConverteSha256Hash(request.Senha);
            _usuarioRepository.InserirAsync(new Usuario(request.NomeCompleto,request.Login,hashSenha,request.Cargo.ToString()));
            await _usuarioRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}