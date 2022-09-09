using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using NailsSys.Core.Entities;
using Xunit;

namespace NailsSys.UnitsTests.Core.Entities
{
    public class ProdutoTests
    {
        private Produto _produto;

        public ProdutoTests()
        {
            _produto = new Produto(
                new Faker().Commerce.ProductName(),
                "P",
                Convert.ToDecimal(new Faker().Commerce.Price(0,50))
            );
        }   

        [Fact]
        public void TestAlterarProduto()
        {
            //Arrange
            var produtoAlterado = new Produto(
                new Faker().Commerce.ProductName(),
                "P",
                Convert.ToDecimal(new Faker().Commerce.Price(0,50))    
            );

            //Act
            _produto.AlterarProduto(
                produtoAlterado.Descricao,
                produtoAlterado.TipoProduto,
                produtoAlterado.Preco    
            );        
            //Assert
            Assert.Equal(_produto.Descricao,produtoAlterado.Descricao);
            Assert.Equal(_produto.TipoProduto,produtoAlterado.TipoProduto);
            Assert.Equal(_produto.Preco,produtoAlterado.Preco);
        }

        [Fact]
        public void TestDescontinuarProduto()
        {
            //Act
            _produto.DescontinuarProduto();
            //Assert
            Assert.True(_produto.Descontinuado);
        }   
    }
}