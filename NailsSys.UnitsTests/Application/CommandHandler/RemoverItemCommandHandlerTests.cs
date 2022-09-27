using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using NailsSys.Application.Commands.ItemAgendamentoCommands.RemoverItem;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class RemoverItemCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IItemAgendamentoRepository> _itemAgendamentoRepository;
        private readonly RemoverItemCommandHandler _removerItemCommandHandler;
        private readonly RemoverItemCommand _removerItemCommand;
        private readonly RemoverItemCommandValidation _removerItemCommandValidation;

        public RemoverItemCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _itemAgendamentoRepository = new Mock<IItemAgendamentoRepository>();
            _unitOfWorks.SetupGet(x => x.ItemAgendamento).Returns(_itemAgendamentoRepository.Object);
            _removerItemCommandHandler = new RemoverItemCommandHandler(_unitOfWorks.Object);
            _removerItemCommand = new RemoverItemCommand(1);
            _removerItemCommandValidation = new RemoverItemCommandValidation();
        }

        [Fact]
        public async Task ItemValido_QuandoExecutado_RemoverItemDoAgendamentoAsync()
        {
            //Arrange
            var item = new ItemAgendamento(1,10,1,1);
            
            _itemAgendamentoRepository.Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(item);
            //Act
            await _removerItemCommandHandler.Handle(_removerItemCommand,new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x=>x.SaveChangesAsync(),Times.Once);
        }

        [Fact]
        public void ItemNaoEncontrado_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _removerItemCommandHandler.Handle(_removerItemCommand,new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Item não encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void ItemInvalido_RetornarExcecoesFluentValidation(int id)
        {
            //Arrange
            var removerItemCommand = new RemoverItemCommand(id);
            //Act
            var result = _removerItemCommandValidation.Validate(removerItemCommand);
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAgendamento.ItemMaiorQueZero) ||
                        erros.Contains(MensagensItensAgendamento.ItemNullVazio));
        }
    }
}