using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;
using NailsSys.Application.Commands.ClienteCommands.AlterarCliente;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarClienteCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly AlterarClienteCommandHandler _alterarClienteCommandHandler;
        private readonly AlterarClienteCommand _alterarClienteCommand;
        private readonly AlterarClienteCommandValidation _alterarClienteCommandHandlerValidation;

        public AlterarClienteCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _alterarClienteCommandHandler = _mocker.CreateInstance<AlterarClienteCommandHandler>();
            _alterarClienteCommand = new Faker<AlterarClienteCommand>()
                .RuleFor(x => x.Id, y => y.Random.Int(0, 10))
                .RuleFor(x => x.NomeCliente, y => y.Person.FullName)
                .RuleFor(x => x.Telefone, y => y.Phone.PhoneNumber())
                .Generate();

            _alterarClienteCommandHandlerValidation = new AlterarClienteCommandValidation();
        }

        [Fact]
        public async Task DadosClienteValido_QuandoExecutado_AlteraCadastroClienteAsync()
        {
            //Arrange
            var cliente = AutoFaker.Generate<Cliente>();

            _mocker.GetMock<IClienteRepository>().Setup(x => x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(cliente);
            //Act
            await _alterarClienteCommandHandler.Handle(_alterarClienteCommand, new CancellationToken());
            //Assert
            _mocker.GetMock<IClienteRepository>().Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void MudarNomedoMetodo()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _alterarClienteCommandHandler.Handle(_alterarClienteCommand, new CancellationToken()));
            Assert.Equal("Nenhum cliente encontrado.", result.Result.Message);
            Assert.NotNull(result.Result);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void NomeClienteInvalido_RetornarExcecoesFluentValidation(string nomeCliente)
        {
            //Arrange
            var alterarClienteCommand = new AlterarClienteCommand { NomeCliente = nomeCliente };
            //Act
            var result = _alterarClienteCommandHandlerValidation.Validate(alterarClienteCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o nome do cliente.") ||
                        erros.Contains("O nome do cliente deve ter no máximo 50 caracteres"));

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
            var alterarClienteCommand = new AlterarClienteCommand { Telefone = telefone };
            //Act
            var result = _alterarClienteCommandHandlerValidation.Validate(alterarClienteCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Informe um telefone válido."));
        }
    }
}