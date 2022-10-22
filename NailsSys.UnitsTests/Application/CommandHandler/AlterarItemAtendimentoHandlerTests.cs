using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Moq;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;
using NailsSys.Application.Commands.AtendimentoCommands.AlterarAtendimento;
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
        private readonly AlterarItemAtendimentoCommand _alterarAtendimentoCommand;
        private readonly AlterarItemAtendimentoCommandHandler _alterarAtendimentoCommandHandler;

        public AlterarItemAtendimentoCommandHandlerTests()
        {
            _atendimentoRepository = new Mock<IAtendimentoRepository>();
            _itemAtendimentoRepository = new Mock<IItemAtendimentoRepository>();
            _unitOfWorks = new Mock<IUnitOfWorks>();

            _unitOfWorks.SetupGet(x => x.Atendimento).Returns(_atendimentoRepository.Object);
            _unitOfWorks.SetupGet(x => x.ItemAtendimento).Returns(_itemAtendimentoRepository.Object);

            _alterarAtendimentoCommand = AutoFaker.Generate<AlterarItemAtendimentoCommand>();
            _alterarAtendimentoCommandHandler = new AlterarItemAtendimentoCommandHandler(_unitOfWorks.Object);
        }

        [Fact]
        public async Task ItemValido_QuandoExecutado_AlterarItemAtendimentoEAtualizarTotalVendaAsync()
        {
            //Arrange
            var atendimento = AutoFaker.Generate<Atendimento>();
            var itemAtendimento = AutoFaker.Generate<ItemAtendimento>();
            _unitOfWorks.Setup(x => x.Atendimento.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(atendimento);
            _unitOfWorks.Setup(x => x.ItemAtendimento.ObterItemAtendimento(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(itemAtendimento);
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
            _unitOfWorks.Setup(x=>x.Atendimento.ObterPorIDAsync(It.IsAny<int>())).ReturnsAsync(AutoFaker.Generate<Atendimento>());
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