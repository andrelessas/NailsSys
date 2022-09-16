using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ItemAgendamentoCommands.AlterarItem;
using NailsSys.Application.Validations;
using NailsSys.Core.DTOs;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarItemCommandHandlerTests
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
            var itemAgendamento = new ItemAgendamento(
                new Faker().Random.Int(),
                new Faker().Random.Int(),
                new Faker().Random.Int(),
                new Faker().Random.Int());

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
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o Item.") == true ||
                        erros.Contains("O Item deve ser maior que 0.") == true);
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
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar a quantidade do item.") == true ||
                        erros.Contains("A Quantidade deve ser maior que 0.") == true);
        }
    }
}