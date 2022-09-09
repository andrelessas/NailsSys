using NailsSys.Core.Entities;
using Xunit;

namespace NailsSys.UnitsTests.Core.Entities
{
    public class ItemAgendamentoTests
    {
        private ItemAgendamento _itemAgendamento;

        public ItemAgendamentoTests()
        {
            _itemAgendamento = new ItemAgendamento(1,1,5,1);
        } 

        [Fact]
        public void TestAtualizarPreco()
        {
            //Act
            _itemAgendamento.AtualizarPreco(10);
            //Assert
            Assert.Equal(50,_itemAgendamento.PrecoInicial);
        }   

        [Fact]
        public void TestAlterarQuantidade()
        {
            //Arrange
            _itemAgendamento.AtualizarPreco(5);
            //Act
            _itemAgendamento.AlterarQuantidade(10);
            //Assert
            Assert.Equal(10,_itemAgendamento.Quantidade);
            Assert.Equal(250,_itemAgendamento.PrecoInicial);
        }
    }
}