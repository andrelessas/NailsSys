using Bogus;
using Moq.AutoMock;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class InserirClienteCommandHandlerTests:TestsConfigurations
    {
        private readonly AutoMocker _mocker;
        private readonly InserirClienteCommandHandler _inserirClienteCommandHandler;
        private readonly InserirClienteCommand _inserirClienteCommand;
        private readonly InserirClienteCommandValidation _inserirClienteCommandValidation;

        public InserirClienteCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _inserirClienteCommandHandler = _mocker.CreateInstance<InserirClienteCommandHandler>();
            _inserirClienteCommand = new Faker<InserirClienteCommand>()
                .RuleFor(x=> x.NomeCliente,y=>y.Person.FullName)
                .RuleFor(x=> x.Telefone,y=>y.Phone.PhoneNumber())
                .Generate();

            _inserirClienteCommandValidation = new InserirClienteCommandValidation();    
        }

        [Fact]
        public async Task DadosClienteValidos_QuandoExcecutado_EfetuaCadastroClienteAsync()
        {
            //Arrange
            var cliente = new Cliente(
                new Faker().Name.FullName(),
                new Faker().Phone.PhoneNumber());                
            //Act
            await _inserirClienteCommandHandler.Handle(_inserirClienteCommand,new CancellationToken());
            //Assert
            _mocker.GetMock<IClienteRepository>().Verify(x=>x.SaveChangesAsync());
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeClienteInvalido_RetornarExcecoesFluentValidation(string nomeCliente)
        {
            //Arrange
            var inserirClienteCommand = new InserirClienteCommand{NomeCliente = nomeCliente};
            //Act
            var result = _inserirClienteCommandValidation.Validate(inserirClienteCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensCliente.NomeClienteNullVazio) ||
                        erros.Contains(MensagensCliente.NomeClienteQuantidadeCaracteresSuperiorAoLimite));

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("4665465464653231546443124686")]
        [InlineData("000000000")]
        public void TelefoneInvalido_RetornarExcecoesFluentValidation(string telefone)
        {
            //Arrange
            var inserirClienteCommand = new InserirClienteCommand{Telefone = telefone};
            //Act
            var result = _inserirClienteCommandValidation.Validate(inserirClienteCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensCliente.TelefoneInvalido));
        }
    }
}