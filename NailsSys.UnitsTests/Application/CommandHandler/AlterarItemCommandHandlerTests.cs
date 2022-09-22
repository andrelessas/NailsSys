using AutoBogus;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarItemCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly AlterarItemCommand _alterarItemCommand;
        private readonly AlterarItemCommandHandler _alterarItemCommandHandler;
        private readonly AlterarItemCommandValidation _alterarItemCommandValidation;

        public AlterarItemCommandHandlerTests()
        {
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _alterarItemCommand = new Faker<AlterarItemCommand>()
                .RuleFor(i => i.Id, v => v.Random.Int())
                .RuleFor(i => i.Quantidade, v => v.Random.Int())
                .Generate();

            _alterarItemCommandHandler = new AlterarItemCommandHandler(_itemAgendamentoRepository.Object);

            _alterarItemCommandValidation = new AlterarItemCommandValidation();
        }
        [Fact]
        public async void ItemValido_QuandoExceutado_AlterarItemDoAgendamento()
        {
            //Arrange
            var itemAgendamento = AutoFaker.Generate<ItemAgendamento>();
            _itemAgendamentoRepository.Setup(ir => ir.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(itemAgendamento);
            //Act
            await _alterarItemCommandHandler.Handle(_alterarItemCommand, new CancellationToken());
            //Assert
            _itemAgendamentoRepository.Verify(ir => ir.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void ItemInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _alterarItemCommandHandler.Handle(_alterarItemCommand,new CancellationToken()));
            Assert.Equal("Item não encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarIdItemInvalido_RetornarExcecaoFluentValidation(int idItem)
        {
            //Arrange
            var itemAgendamentoCommand = new AlterarItemCommand{Id = idItem};
            //Act
            var result = _alterarItemCommandValidation.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains("Necessário informar o Item.") ||
                        erros.Contains("O Item deve ser maior que 0."));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarQuantidadeInvalida_RetornarExcecaoFluentValidation(int quantidade)
        {
            //Arrange
            var itemAgendamentoCommand = new AlterarItemCommand{Quantidade = quantidade};
            //Act
            var result = _alterarItemCommandValidation.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains("Necessário informar a quantidade do item.") ||
                        erros.Contains("A Quantidade deve ser maior que 0."));
        }
    }
}