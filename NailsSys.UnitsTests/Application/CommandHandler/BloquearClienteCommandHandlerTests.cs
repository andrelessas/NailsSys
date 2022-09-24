using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.ClienteCommands.BloquearCliente;
using NailsSys.Application.Commands.ClienteCommands.InserirCliente;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class BloquearClienteCommandHandlerTests:TestsConfigurations
    {
        private readonly AutoMocker _mocker;
        private readonly BloquearClienteCommandHandler _bloquearClienteCommandHandler;
        private readonly BloquearClienteCommand _bloquearClienteCommand;
        private readonly BloquearClienteCommandValidation _bloquearClienteCommandValidation;

        public BloquearClienteCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _bloquearClienteCommandHandler = _mocker.CreateInstance<BloquearClienteCommandHandler>();
            _bloquearClienteCommand = new BloquearClienteCommand(1);

            _bloquearClienteCommandValidation = new BloquearClienteCommandValidation();    
        }

        [Fact]
        public async Task InformadoIdClienteValido_QuandoExecutado_BloquearClienteAsync()
        {
            //Arrange
            var cliente = new Cliente("teste","9999999999999");

            _mocker.GetMock<IClienteRepository>().Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(cliente);
            //Act
            await _bloquearClienteCommandHandler.Handle(_bloquearClienteCommand,new CancellationToken());
            //Assert
            _mocker.GetMock<IClienteRepository>().Verify(x => x.SaveChangesAsync(),Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void TelefoneInvalido_RetornarExcecoesFluentValidation(int id)
        {
            //Arrange
            var bloquearClienteCommand = new BloquearClienteCommand(id);
            //Act
            var result = _bloquearClienteCommandValidation.Validate(bloquearClienteCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensCliente.IdClienteNaoInformadoAoBloquearCLiente));
        }

    }
}