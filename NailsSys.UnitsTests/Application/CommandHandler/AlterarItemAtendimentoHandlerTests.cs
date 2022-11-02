using Bogus;
using Moq;
using NailsSys.Application.Commands.ItemAtendimentoCommands.AlterarItem;
using NailsSys.Application.Validations;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.CommandHandler
{
    public class AlterarItemAtendimentoCommandHandlerTests:TestsConfigurations
    {
        private readonly Mock<IAtendimentoRepository> _atendimentoRepository;
        private readonly Mock<IItemAtendimentoRepository> _itemAtendimentoRepository;
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private AlterarItemAtendimentoCommand _alterarAtendimentoCommand;
        private readonly AlterarItemAtendimentoCommandHandler _alterarAtendimentoCommandHandler;
        private Atendimento _atendimento;
        private ItemAtendimento _itemAtendimento;

        public AlterarItemAtendimentoCommandHandlerTests()
        {
            _atendimentoRepository = new Mock<IAtendimentoRepository>();
            _itemAtendimentoRepository = new Mock<IItemAtendimentoRepository>();
            _unitOfWorks = new Mock<IUnitOfWorks>();

            _unitOfWorks.SetupGet(x => x.Atendimento).Returns(_atendimentoRepository.Object);
            _unitOfWorks.SetupGet(x => x.ItemAtendimento).Returns(_itemAtendimentoRepository.Object);

            _alterarAtendimentoCommand = new AlterarItemAtendimentoCommand(1,1,1,1);

            _alterarAtendimentoCommandHandler = new AlterarItemAtendimentoCommandHandler(_unitOfWorks.Object);

            _atendimento = new Atendimento(1,DateTime.Now,1,DateTime.Now,DateTime.Now);

            _itemAtendimento = new ItemAtendimento(1,1,1,1);
        }

        [Fact]
        public async Task ItemValido_QuandoExecutado_AlterarItemAtendimentoEAtualizarTotalVendaAsync()
        {
            //Arrange
            _unitOfWorks.Setup(x => x.Atendimento.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_atendimento);
            _unitOfWorks.Setup(x => x.ItemAtendimento.ObterItemAtendimento(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(_itemAtendimento);
            //Act
            await _alterarAtendimentoCommandHandler.Handle(_alterarAtendimentoCommand, new CancellationToken());
            //Assert
            _unitOfWorks.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void AtendimentoNaoEncontrado_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _alterarAtendimentoCommandHandler.Handle(_alterarAtendimentoCommand, new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.NotEmpty(result.Result.Message);
            Assert.Equal("Atendimento não encontrado.", result.Result.Message);
        }

        [Fact]
        public void ItemAtendimentoNaoEncontrado_QuandoExecutado_RetornarExcecao()
        {
            //Arrange 
            _unitOfWorks.Setup(x=>x.Atendimento.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(_atendimento);
            //Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _alterarAtendimentoCommandHandler.Handle(_alterarAtendimentoCommand, new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.NotEmpty(result.Result.Message);
            Assert.Equal("O atendimento não possui item informado.", result.Result.Message);
        }
        [Theory]
        [InlineData(0,0,0,0)]
        [InlineData(-1,-1,-1,-1)]
        public void Invalido_RetornarExcecoesFluentValidations(int idAtendimento, int item, int idProduto, int quantidade)
        {
            //Arrange 
            var valid = new AlterarItemAtendimentoCommandValidation();
            //Act
            var alterarItemAtendimentoCommandValidation = valid.Validate(new AlterarItemAtendimentoCommand(idAtendimento,item,idProduto,quantidade));
            //Assert
            Assert.False(alterarItemAtendimentoCommandValidation.IsValid);
            var erros = ObterListagemErro(alterarItemAtendimentoCommandValidation);
            Assert.True(erros.Contains(MensagensItensAtendimento.IdAtendimentoMaiorQueZero) ||
                        erros.Contains(MensagensItensAtendimento.IdAtendimentoNullVazio) ||
                        erros.Contains(MensagensItensAtendimento.IdProdutoMaiorQueZero) ||
                        erros.Contains(MensagensItensAtendimento.IdProdutoNullVazio) ||
                        erros.Contains(MensagensItensAtendimento.ItemMaiorQueZero) ||
                        erros.Contains(MensagensItensAtendimento.ItemNullVazio) ||
                        erros.Contains(MensagensItensAtendimento.QuantidadeMaiorQueZero) ||
                        erros.Contains(MensagensItensAtendimento.QuantidadeNullVazio));
        }
    }
}