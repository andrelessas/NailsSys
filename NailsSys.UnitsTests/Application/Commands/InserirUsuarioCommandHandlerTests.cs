using Bogus;
using Moq;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Enums;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Services;
using Xunit;

namespace NailsSys.UnitsTests.Application.Commands
{
    public class InserirUsuarioCommandHandlerTests
    {
        private InserirUsuarioCommand _inserirUsuarioCommand;
        private FluentValidation.Results.ValidationResult _validationResult;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IAutenticacaoService> _autenticacaoServiceMock;

        public InserirUsuarioCommandHandlerTests()
        {
            _inserirUsuarioCommand = new Faker<InserirUsuarioCommand>()
                .RuleFor(u => u.NomeCompleto, "Andre lessas")
                .RuleFor(u => u.Login, "andre")
                .RuleFor(u => u.Senha, f => f.Internet.Password(length: 10, prefix: "a@"))
                .RuleFor(u => u.Cargo, Cargos.Atendente)
                .Generate();

            var usuarioValidator = new InserirUsuarioCommandValidation();
            _validationResult = usuarioValidator.Validate(_inserirUsuarioCommand);

            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _autenticacaoServiceMock = new Mock<IAutenticacaoService>();
        }
        [Fact]
        public async Task DadosDeUsuarioValidos_ExecutaCadastroDeNovoUsuarioAsync()
        {
            //Arrange   
            var senhaHash = "c131c63023da974fb20016d91331797ca214f376ba64debf3a0c4aa9049e965c";
            var inserirUsuarioCommandHandler = new InserirUsuarioCommandHandler(_usuarioRepositoryMock.Object, _autenticacaoServiceMock.Object);

            _autenticacaoServiceMock.Setup(x => x.ConverteSha256Hash(It.IsAny<String>())).Returns(senhaHash);
            //Act
            await inserirUsuarioCommandHandler.Handle(_inserirUsuarioCommand, new CancellationToken());
            //Assert   
            Assert.True(_validationResult.IsValid);
            _usuarioRepositoryMock.Verify(x => x.InserirAsync(It.IsAny<Usuario>()), Times.Once);
        }
    }
}