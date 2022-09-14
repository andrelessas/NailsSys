using NailsSys.Application.Commands.UsuarioCommands.InserirUsuario;
using NailsSys.Application.Validations;
using NailsSys.Core.Enums;
using Xunit;

namespace NailsSys.UnitsTests.Application.Validations
{
    public class UsuarioValidationTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarNomeCompletoUsuarioInvalido_RetornarExcecoesFluentValidations(string nomeCompleto)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{NomeCompleto = nomeCompleto};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o nome completo.") == true ||
                        erros.Contains("O nome do usuário deve conter no mínimo 5 caracteres e no máximo 70 caracteres.") == true);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarLoginUsuarioInvalido_RetornarExcecoesFluentValidations(string login)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Login = login};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o login do usuário.") == true ||
                        erros.Contains("O login do usuário deve conter no mínimo 5 caracteres e no máximo 15 caracteres.") == true);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("aa")]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public void TestarSenhaUsuarioInvalido_RetornarExcecoesFluentValidations(string senha)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Senha = senha};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar a senha do usuário.") == true ||
                        erros.Contains("Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial") == true);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(0)]
        public void TestarCargoUsuarioInvalido_RetornarExcecoesFluentValidations(int cargo)
        {
            // Arrange 
            var usuario = new InserirUsuarioCommand{Cargo = (Cargos)cargo};
            var usuarioValidator = new InserirUsuarioCommandValidation();
            // Act
            var result = usuarioValidator.Validate(usuario);
            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.True(erros.Contains("Necessário informar o cargo do usuário.") == true ||
                        erros.Contains("Cargo do usuário inválido, o cargo deve ser Adminitrador, Gerente ou Atendente.") == true);
        }
    }
}