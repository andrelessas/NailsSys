using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoBogus;
using Bogus;
using Moq;
using NailsSys.Application.Queries.AgendamentoQueries.ObterAgendamentosHoje;
using NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoPorId;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterAtendimentoPorIdQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IAtendimentoRepository> _atendimentoRepository;
        private readonly ObterAtendimentoPorIdQueriesHandler _obterAtendimentoPorIdQueriesHandler;

        public ObterAtendimentoPorIdQueriesHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _atendimentoRepository = new Mock<IAtendimentoRepository>();

            _unitOfWorks.SetupGet(x=>x.Atendimento).Returns(_atendimentoRepository.Object);

            _obterAtendimentoPorIdQueriesHandler = new ObterAtendimentoPorIdQueriesHandler(_unitOfWorks.Object,IMapper);
        }
        
        [Fact]
        public async void IdAtendimentoValido_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            var atendimento = new Atendimento(1,
                DateTime.Now,
                1,
                DateTime.Now,
                DateTime.Now);

            _atendimentoRepository.Setup(x=>x.ObterPorId(It.IsAny<int>())).ReturnsAsync(atendimento);
            //Act
            var result = await _obterAtendimentoPorIdQueriesHandler.Handle(new ObterAtendimentoPorIDQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.DataAtendimento,atendimento.DataAtendimento);
        }

        [Fact]
        public void AtendimentoInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterAtendimentoPorIdQueriesHandler.Handle(new ObterAtendimentoPorIDQueries(1), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal(result.Result.Message,"Atendimento não encontrado.");
        }
    }
}