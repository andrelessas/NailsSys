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
        private readonly IUnitOfWorks _unitOfWorks;
        private readonly IAutenticacaoService _autenticacaoService;

        public InserirUsuarioCommandHandler(IUnitOfWorks unitOfWorks,
                                            IAutenticacaoService autenticacaoService)
        {
            _unitOfWorks = unitOfWorks;
            _autenticacaoService = autenticacaoService;
        }
        public async Task<Unit> Handle(InserirUsuarioCommand request, CancellationToken cancellationToken)
        {
            var hashSenha = _autenticacaoService.ConverteSha256Hash(request.Senha);
            _unitOfWorks.Usuario.InserirAsync(new Usuario(request.NomeCompleto,request.Login,hashSenha,request.Cargo.ToString()));
            await _unitOfWorks.SaveChangesAsync();
            return Unit.Value;
        }
    }
}