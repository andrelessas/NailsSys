using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.AgendamentoCommands.CancelamentoAgendamento;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class CancelarAgendamentoCommandHandlerTests:TestsConfigurations
    {
        private readonly AutoMocker _mocker;
        private readonly CancelarAgendamentoCommandHandler _cancelarAgendamentoCommandHandler;
        private readonly CancelarAgendamentoCommand _cancelarAgendamentoCommand;
        private readonly CancelarAgendamentoCommandValidation _cancelarAgendamentoCommandValidation;

        public CancelarAgendamentoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _cancelarAgendamentoCommandHandler = _mocker.CreateInstance<CancelarAgendamentoCommandHandler>();
            _cancelarAgendamentoCommand = new CancelarAgendamentoCommand { Id = 10 };
            _cancelarAgendamentoCommandValidation = new CancelarAgendamentoCommandValidation();
        }

        [Fact]
        public async Task InformadoIdAgendamentoValido_QuandoExecutado_CancelarAgendamentoAsync()
        {
            //Arrange
            var agendamento = AutoFaker.Generate<Agendamento>();
            _mocker.GetMock<IAgendamentoRepository>().Setup(x=> x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
            //Act
            await _cancelarAgendamentoCommandHandler.Handle(_cancelarAgendamentoCommand, new CancellationToken());
            //Assert
            _mocker.GetMock<IAgendamentoRepository>().Verify(x=> x.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void InformadoIdAgendamentoQueNaoExiste_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _cancelarAgendamentoCommandHandler.Handle(_cancelarAgendamentoCommand,new CancellationToken()));
            Assert.Equal("Nenhum agendamento encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void IdAgendamentoInvalido_RetornarExcecaoFluentValidation(int id)
        {
            //Arrange
            var cancelarAgendamentoCommand = new CancelarAgendamentoCommand{Id = id};
            //Act
            var result = _cancelarAgendamentoCommandValidation.Validate(cancelarAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.IdAgendamentoNaoInformadoCancelarAgendamento));
        }


    }
}