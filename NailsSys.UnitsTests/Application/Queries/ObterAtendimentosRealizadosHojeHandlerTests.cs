using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoRealizadosHoje;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterAtendimentosRealizadosHojeQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUnitOfWorks> _unitOfWorks;
        private readonly Mock<IAtendimentoRepository> _atendimentoRepository;
        private readonly ObterAtendimentosRealizadosHojeQueriesHandler _obterAtendimentosRealizadosHojeQueriesHandler;

        public ObterAtendimentosRealizadosHojeQueriesHandlerTests()
        {
            _unitOfWorks = new Mock<IUnitOfWorks>();
            _atendimentoRepository = new Mock<IAtendimentoRepository>();

            _unitOfWorks.SetupGet(x=>x.Atendimento).Returns(_atendimentoRepository.Object);
            _obterAtendimentosRealizadosHojeQueriesHandler = new ObterAtendimentosRealizadosHojeQueriesHandler(_unitOfWorks.Object,IMapper);
        }
        
        [Fact]
        public async void AtendimentosHoje_QuandoExecutado_RetornarObjeto()
        {
            //Arrange

            var atendimento = new List<Atendimento>();
            for (int i = 0; i < 5; i++)
            {
                atendimento.Add(new Atendimento(1,
                    DateTime.Now,
                    1,
                    DateTime.Now,
                    DateTime.Now));
            }
            
            _atendimentoRepository.Setup(x=>x.ObterAtendimentosRealizadosHoje()).ReturnsAsync(atendimento);
            //Act
            var result = await _obterAtendimentosRealizadosHojeQueriesHandler.Handle(new ObterAtendimentosRealizadosHojeQueries(),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Count(),atendimento.Count());
        }

        [Fact]
        public void AtendimentoInvalido_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterAtendimentosRealizadosHojeQueriesHandler.Handle(new ObterAtendimentosRealizadosHojeQueries(), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Nenhum atendimento foi realizado hoje.",result.Result.Message);
        }
    }
}