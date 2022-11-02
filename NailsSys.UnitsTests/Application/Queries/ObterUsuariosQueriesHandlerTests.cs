using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Moq;
using NailsSys.Application.Queries.ClienteQueries.ObterClientes;
using NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios;
using NailsSys.Application.Validations;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Core.Models;
using NailsSys.Core.Notificacoes;
using NailsSys.UnitsTests.Application.Configurations;
using Xunit;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class ObterUsuariosQueriesHandlerTests:TestsConfigurations
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepository;
        private readonly ObterUsuariosQueriesHandler _obterUsuariosQueriesHandler;
        private readonly ObterUsuariosQueriesValidation _obterUsuariosQueriesValidation;

        public ObterUsuariosQueriesHandlerTests()
        {
            _usuarioRepository = new Mock<IUsuarioRepository>();
            _obterUsuariosQueriesHandler = new ObterUsuariosQueriesHandler(_usuarioRepository.Object,IMapper);
            _obterUsuariosQueriesValidation = new ObterUsuariosQueriesValidation();
        }
        
        [Fact]
        public async void ParametrosDeUsuarioValido_QuandoExecutado_RetornarObjeto()
        {
            //Arrange
            List<Usuario> usuarios = new List<Usuario>();
            for (int i = 0; i < 5; i++)
            {
                usuarios.Add(new Usuario(new Faker().Person.FullName,
                    new Faker().Person.FirstName,
                    new Faker().Internet.Password(),
                    "G"));
            }            
            
            var pagination = new Faker<PaginationResult<Usuario>>()
                .RuleFor(x => x.Data, usuarios)
                .Generate();

            _usuarioRepository.Setup(x=>x.ObterTodosAsync(It.IsAny<int>())).ReturnsAsync(pagination);
            //Act
            var result = await _obterUsuariosQueriesHandler.Handle(new ObterUsuariosQueries(1),new CancellationToken());
            //Assert
            Assert.NotNull(result);
            Assert.Equal(usuarios.Count(),result.Data.Count());
            foreach (var usuario in usuarios)
            {
                Assert.Contains<UsuarioViewModel>(result.Data,x=>x.Cargo == usuario.Cargo);
                Assert.Contains<UsuarioViewModel>(result.Data,x=>x.Login == usuario.Login);
                Assert.Contains<UsuarioViewModel>(result.Data,x=>x.NomeCompleto == usuario.NomeCompleto);
            }
        }

        [Fact]
        public void NaoExisteUsuarios_QuandoExecutado_RetornarExcecao()
        {
            //Arrange - Act - Assert
            var result = Assert.ThrowsAsync<ExcecoesPersonalizadas>(() => _obterUsuariosQueriesHandler.Handle(new ObterUsuariosQueries(1), new CancellationToken()));
            Assert.NotNull(result.Result);
            Assert.Equal("Nenhum usuário encontrado.",result.Result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void ParametroPageInvalido_RetornarExcecoesFluentValidations(int page)
        {
            //Arrange - Act
            var result = _obterUsuariosQueriesValidation.Validate(new ObterClientesQueries(page));
            //Assert
            Assert.False(result.IsValid);
            var erros = ObterListagemErro(result);
            Assert.True(erros.Contains(MensagensPaginacao.PageNullVazio) ||
                        erros.Contains(MensagensPaginacao.PageMaiorQueZero));
        }
    }
}