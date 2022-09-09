using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.UsuarioCommands.LoginUsuarioCommand;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Services;
using Xunit;

namespace NailsSys.UnitsTests.Application.Commands
{
    public class LoginUsuarioCommandHandlerTest
    {
        private Mock<IUsuarioRepository> _usuarioRepository;
        private Mock<IAutenticacaoService> _autenticacaoService;
        private LoginUsuarioCommand _usuarioLoginCommand;
        private Usuario _usuario;
        private LoginUsuarioCommandHandler _loginUsuarioCommandHandler;

        public LoginUsuarioCommandHandlerTest()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _autenticacaoService = new Mock<IAutenticacaoService>();

            _usuarioLoginCommand = new Faker<LoginUsuarioCommand>()
                .RuleFor(x => x.Usuario,y => y.Name.FirstName())
                .RuleFor(x => x.Senha,y=> y.Internet.Password(10))
                .Generate();

            _usuario = new Usuario("",new Faker().Name.FirstName(),"","");                

            _loginUsuarioCommandHandler = new LoginUsuarioCommandHandler(_usuarioRepository.Object,_autenticacaoService.Object);
        }
        [Fact]
        public async Task DadosLoginValidos_ExecutaLogin_RetornarObjeto()
        {
            //Arrange
            var senhaHash = "c131c63023da974fb20016d91331797ca214f376ba64debf3a0c4aa9049e965c";
            _autenticacaoService.Setup(x => x.ConverteSha256Hash(It.IsAny<string>())).Returns(senhaHash);
            _usuarioRepository.Setup(x => x.ObterUsuarioPorIdLoginSenha(It.IsAny<int>(),It.IsAny<string>(),It.IsAny<string>())).ReturnsAsync(_usuario);
            _autenticacaoService.Setup(x => x.GerarToken(It.IsAny<int>(),It.IsAny<string>())).Returns(senhaHash);
            //Act
            var usuarioViewModel = await _loginUsuarioCommandHandler.Handle(_usuarioLoginCommand,new CancellationToken());
            //Assert
            Assert.NotNull(usuarioViewModel);
            Assert.Equal(_usuario.Login,usuarioViewModel.Usuario);
            Assert.Equal(senhaHash,usuarioViewModel.Token);
        }

        [Fact]
        public async void DadosLoginInvalidos_ExecutaLogin_RetornarNull()
        {
            //Arrange
            var senhaHash = "c131c63023da974fb20016d91331797ca214f376ba64debf3a0c4aa9049e965c";
            _autenticacaoService.Setup(x => x.ConverteSha256Hash(It.IsAny<string>())).Returns(senhaHash);
            _usuarioRepository.Setup(x => x.ObterUsuarioPorIdLoginSenha(It.IsAny<int>(),It.IsAny<string>(),It.IsAny<string>()));
            //Act
            var usuarioViewModel = await _loginUsuarioCommandHandler.Handle(_usuarioLoginCommand,new CancellationToken());
            //Assert
            Assert.Null(usuarioViewModel);
            _usuarioRepository.Verify(x => x.ObterUsuarioPorIdLoginSenha(It.IsAny<int>(),It.IsAny<string>(),It.IsAny<string>()),Times.Once);
        }
    }
}