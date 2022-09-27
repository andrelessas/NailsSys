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
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarAgendamentoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IAgendamentoRepository> _agendamentoRepository;
        private readonly AlterarAgendamentoCommandHandler _alterarAgendamentoCommandHandler;
        private readonly AlterarAgendamentoCommand _alterarAgendamentoCommand;
        private readonly AlterarAgendamentoCommandValidation _alterarAgendamentoCommandValidation;

        public AlterarAgendamentoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _agendamentoRepository = new Mock<IAgendamentoRepository>();
            _unitOfWorks.SetupGet(x => x.Agendamento).Returns(_agendamentoRepository.Object);        
            _alterarAgendamentoCommandHandler = new AlterarAgendamentoCommandHandler(_unitOfWorks.Object);
            
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

            _agendamentoRepository.Setup(a => a.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(agendamento);
            //Act
            await _alterarAgendamentoCommandHandler.Handle(_alterarAgendamentoCommand, new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x => x.SaveChangesAsync());
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
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.DataAtendimentoNullVazia) ||
                        erros.Contains(MensagensAgendamento.DataAtendimentoInvalida));
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
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensAgendamento.HorarioInvalido) ||
                        erros.Contains(MensagensAgendamento.HorarioNullVazio) ||
                        erros.Contains(MensagensAgendamento.TerminoAtendimentoMaiorQueInicioDoAtendimento));
        }               
    }
}