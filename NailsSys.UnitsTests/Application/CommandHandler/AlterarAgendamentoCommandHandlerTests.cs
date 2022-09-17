using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarAgendamentoCommandHandlerTests
    {
        private readonly AutoMocker _mocker;
        private readonly AlterarAgendamentoCommandHandler _alterarAgendamentoCommandHandler;
        private readonly AlterarAgendamentoCommand _alterarAgendamentoCommand;
        private readonly AlterarAgendamentoCommandValidation _alterarAgendamentoCommandValidation;

        public AlterarAgendamentoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _alterarAgendamentoCommandHandler = _mocker.CreateInstance<AlterarAgendamentoCommandHandler>();
            
            _alterarAgendamentoCommand = new Faker<AlterarAgendamentoCommand>()
                .RuleFor(a => a.Id, v => v.Random.Int(0, 50))
                .RuleFor(a => a.IdCliente, v => v.Random.Int(0, 50))
                .RuleFor(a => a.DataAtendimento, v => v.Date.Recent())
                .RuleFor(a => a.InicioPrevisto, v => v.Date.Recent())
                .RuleFor(a => a.TerminoPrevisto, v => v.Date.Recent())
                .Generate();
            
            _alterarAgendamentoCommandValidation = new AlterarAgendamentoCommandValidation();
        }

        [Fact]
        public async Task AlterarAgendamentoCommandValido_QuandoExecutado_AlterarAgendamentoAsync()
        {
            //Arrange            
            var agendamento = new Agendamento(
                _alterarAgendamentoCommand.IdCliente,
                _alterarAgendamentoCommand.DataAtendimento,
                _alterarAgendamentoCommand.InicioPrevisto,
                _alterarAgendamentoCommand.TerminoPrevisto);

            _mocker.GetMock<IAgendamentoRepository>().Setup(a => a.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
            //Act
            await _alterarAgendamentoCommandHandler.Handle(_alterarAgendamentoCommand, new CancellationToken());
            //Assert
            _mocker.GetMock<IAgendamentoRepository>().Verify(x => x.SaveChangesAsync());
        }

        [Fact]
        public void AlterarAgendamentoCommandInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _alterarAgendamentoCommandHandler.Handle(_alterarAgendamentoCommand,new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Agendamento não encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData("2022-9-15 10:30:40")]
        public void DataAtendimentoInvalida_RetornarExcecaoFluentValidation(DateTime dataAtendimento)
        {
            //Arrange
            var alterarAgendamentoCommand = new AlterarAgendamentoCommand{DataAtendimento = dataAtendimento};
            //Act
            var result = _alterarAgendamentoCommandValidation.Validate(alterarAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Data inválida.") ||
                        erros.Contains("Necessário informar uma Data válida."));
        }               

        [Theory]
        [InlineData("2022-9-10 10:30:15","2022-9-10 10:40:15")]
        [InlineData("2022-9-17 10:30:15","2022-9-17 10:20:15")]
        public void HorariosInvalidos_RetornarExcecaoFluentValidation(DateTime inicioPrevisto,DateTime terminoPrevisto)
        {
            //Arrange
            var alterarAgendamentoCommand = new AlterarAgendamentoCommand{InicioPrevisto = inicioPrevisto,TerminoPrevisto = terminoPrevisto};
            //Act
            var result = _alterarAgendamentoCommandValidation.Validate(alterarAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(x => x.ErrorMessage).ToList();
            Assert.True(erros.Contains("Horário inválido.") ||
                        erros.Contains("Necessário informar um horário válido.") ||
                        erros.Contains("O término do agendamento não pode ser maior que o inicio do agendamento."));
        }               
    }
}