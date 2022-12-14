using Bogus;
using Moq;
using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Enums;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.Core.Services;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class InserirUsuarioCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private InserirUsuarioCommand _inserirUsuarioCommand;
        private FluentValidation.Results.ValidationResult _validationResult;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IAutenticacaoService> _autenticacaoServiceMock;
        private readonly InserirUsuarioCommandHandler _inserirUsuarioCommandHandler;

        public InserirUsuarioCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
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

            _unitOfWorks.SetupGet(x => x.Usuario).Returns(_usuarioRepositoryMock.Object);

            _inserirUsuarioCommandHandler = new InserirUsuarioCommandHandler(_unitOfWorks.Object, _autenticacaoServiceMock.Object);

        }
        [Fact]
        public async Task DadosDeUsuarioValidos_ExecutaCadastroDeNovoUsuarioAsync()
        {
            //Arrange   
            var senhaHash = "c131c63023da974fb20016d91331797ca214f376ba64debf3a0c4aa9049e965c";

            _autenticacaoServiceMock.Setup(x => x.ConverteSha256Hash(It.IsAny<String>())).Returns(senhaHash);
            //Act
            await _inserirUsuarioCommandHandler.Handle(_inserirUsuarioCommand, new CancellationToken());
            //Assert   
            _usuarioRepositoryMock.Verify(x => x.InserirAsync(It.IsAny<Usuario>()), Times.Once);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarNomeCompletoUsuarioInvalido_RetornarExcecoesFluentValidations(string nomeCompleto)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{NomeCompleto = nomeCompleto};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensUsuario.NomeCompletoCurtoOuLongo) ||
                        erros.Contains(MensagensUsuario.NomeCompletoNullVazio));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarLoginUsuarioInvalido_RetornarExcecoesFluentValidations(string login)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Login = login};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensUsuario.LoginCurtoOuLongo) ||
                        erros.Contains(MensagensUsuario.LoginNullVazio));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarSenhaUsuarioInvalido_RetornarExcecoesFluentValidations(string senha)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Senha = senha};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensUsuario.SenhaNullVazio) ||
                        erros.Contains(MensagensUsuario.SenhaFraca));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void TestarCargoUsuarioInvalido_RetornarExcecoesFluentValidations(int cargo)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Cargo = (Cargos)cargo};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensUsuario.CargoNullVazio) ||
                        erros.Contains(MensagensUsuario.CargoInvalido));
        }
    }
}