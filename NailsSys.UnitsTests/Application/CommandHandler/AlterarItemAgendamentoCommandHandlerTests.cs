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
    public class AlterarItemAgendamentoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly AlterarItemAgendamentoCommand _alterarItemCommand;
        private readonly AlterarItemAgendamentoCommandHandler _alterarItemCommandHandler;
        private readonly AlterarItemCommandValidation _alterarItemCommandValidation;

        public AlterarItemAgendamentoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _alterarItemCommand = new Faker<AlterarItemAgendamentoCommand>()
                .RuleFor(i => i.Id, v => v.Random.Int())
                .RuleFor(i => i.Quantidade, v => v.Random.Int())
                .Generate();

            _unitOfWorks.SetupGet(x => x.ItemAgendamento).Returns(_itemAgendamentoRepository.Object);
            _alterarItemCommandHandler = new AlterarItemAgendamentoCommandHandler(_unitOfWorks.Object);
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
            _unitOfWorks.Verify(ir => ir.SaveChangesAsync(),Times.Once);
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
            var itemAgendamentoCommand = new AlterarItemAgendamentoCommand{Id = idItem};
            //Act
            var result = _alterarItemCommandValidation.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.ItemMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.ItemNullVazio));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(null)]
        [InlineData(-10)]
        public void ValidarQuantidadeInvalida_RetornarExcecaoFluentValidation(int quantidade)
        {
            //Arrange
            var itemAgendamentoCommand = new AlterarItemAgendamentoCommand{Quantidade = quantidade};
            //Act
            var result = _alterarItemCommandValidation.Validate(itemAgendamentoCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.QuantidadeMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.QuantidadeNullVazio));
        }
    }
}