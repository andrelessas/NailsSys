using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Commands.ItemAtendimentoCommands.RemoverItem;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class RemoverItemAtendimentoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IAtendimentoRepository> _atendimentoRepository;
        private readonly Mock<IItemAtendimentoRepository> _atendimentoItemRepository;
        private readonly RemoverItemAtendimentoCommandHandler _removerItemAtendimentoCommandHandler;
        private readonly RemoverItemAtendimentoCommandValidation _removerItemAtendimentoCommandValidation;
        private Atendimento _atendimento;
        private ItemAtendimento _itemAtendimento;

        public RemoverItemAtendimentoCommandHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _atendimentoRepository = new Mock<IAtendimentoRepository>();
            _atendimentoItemRepository = new Mock<IItemAtendimentoRepository>();
            _removerItemAtendimentoCommandHandler = new RemoverItemAtendimentoCommandHandler(_unitOfWorks.Object);
            _removerItemAtendimentoCommandValidation = new RemoverItemAtendimentoCommandValidation();
            _unitOfWorks.SetupGet(x=> x.Atendimento).Returns(_atendimentoRepository.Object);
            _unitOfWorks.SetupGet(x=> x.ItemAtendimento).Returns(_atendimentoItemRepository.Object);
            _atendimento = new Atendimento(1,DateTime.Now,1,DateTime.Now,DateTime.Now);
            _itemAtendimento = new ItemAtendimento(1,1,1,1);
        }
        
        [Fact]
        public async Task ProdutoValido_QuandoExecutado_RemoveItemEAtualizaTotalAtendimentoAsync()
        {
            //Arrange
            _atendimentoItemRepository.Setup(x=>x.ObterItemAtendimento(It.IsAny<int>(),It.IsAny<int>())).ReturnsAsync(_itemAtendimento);
            _atendimentoRepository.Setup(x=>x.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_atendimento);
            //Act
            await _removerItemAtendimentoCommandHandler.Handle(new RemoverItemAtendimentoCommand(1,1),new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x=>x.BeginTransactionAsync(),Times.Once);
            _unitOfWorks.Verify(x=>x.SaveChangesAsync(),Times.Once);
            _unitOfWorks.Verify(x=>x.CommitAsync(),Times.Once);
        }

        [Fact]
        public void ItemNaoEncontrado_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _removerItemAtendimentoCommandHandler.Handle(new RemoverItemAtendimentoCommand(1,1),new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.NotEmpty(result.Result.Message);
            Assert.Equal("Nenhum item encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(-1,-1)]
        [InlineData(0,0)]
        public void Invalido_RetornarExcecoesFluentValidations(int IdAtendimento, int item)
        {
            //Arrange - Act
            var result = _removerItemAtendimentoCommandValidation.Validate(new RemoverItemAtendimentoCommand(IdAtendimento,item));
            //Assert
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensItensAtendimento.IdAtendimentoMaiorQueZero) ||
                        erros.Contains(MensagensItensAtendimento.IdAtendimentoNullVazio));
        }
    }
}