using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Services;

namespace NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, LoginUsuarioViewModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IAutenticacaoService _autenticacaoService;

        public LoginUsuarioCommandHandler(IUsuarioRepository usuarioRepository,
                                          IAutenticacaoService autenticacaoService)
        {
            _usuarioRepository = usuarioRepository;
            _autenticacaoService = autenticacaoService;
        }
        public async Task<LoginUsuarioViewModel> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var senhaHash = _autenticacaoService.ConverteSha256Hash(request.Senha);
            var usuario = await _usuarioRepository.ObterUsuarioPorIdLoginSenha(request.Id,request.Usuario, senhaHash);
            
            if (usuario == null)
                return null;
            
            var token = _autenticacaoService.GerarToken(usuario.Id,usuario.Cargo);
            return new LoginUsuarioViewModel(usuario.Login,token);
        }
    }
}