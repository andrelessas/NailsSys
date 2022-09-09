using Bogus;
using NailsSys.Core.Entities;
using Xunit;

namespace NailsSys.UnitsTests.Core.Entities
{
    public class ClienteTests
    {
        private Cliente _cliente;

        public ClienteTests()
        {            
            _cliente = new Cliente(
                new Faker().Name.FullName(),
                new Faker().Phone.PhoneNumber());
        }

        [Fact]
        public void TestAlterarCliente()
        {
            //Arrange
            var novoCliente = new Cliente(
                new Faker().Name.FullName(),
                new Faker().Phone.PhoneNumber());                
            //Act
            _cliente.AlterarCliente(novoCliente.NomeCliente,novoCliente.Telefone);
            //Assert
            Assert.Equal(novoCliente.NomeCliente,_cliente.NomeCliente);
            Assert.Equal(novoCliente.Telefone,_cliente.Telefone);
        }

        [Fact]
        public void TestBloquerCliente()
        {        
            //Act
            _cliente.BloquearCliente();
            //Assert
            Assert.True(_cliente.Bloqueado);
        }
    }
}