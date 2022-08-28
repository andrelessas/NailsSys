// using Bogus;
// using NailsSys.Core.DTOs;

// namespace NailsSys.Tests.ModelFakes
// {
//     public class ProdutoFakers
//     {
//         public ProdutoDTO ObterProdutoDTOFaker()
//         {
//             var produtoDTO = new Faker<ProdutoDTO>()
//                                 .RuleFor(x=>x.Descricao,y=>y.Commerce.Product())
//                                 .RuleFor(x => x.TipoProduto,"P")
//                                 .RuleFor(x=>x.Preco,y=>y.Finance.Random.Decimal())
//                                 .Generate();
//             return produtoDTO;
//         }

//         public List<ProdutoDTO> ObterProdutoDTOFaker(int quantidade)
//         {
//             var produtoDTO = new Faker<ProdutoDTO>()
//                                 .RuleFor(x=>x.Descricao,y=>y.Commerce.Product())
//                                 .RuleFor(x => x.TipoProduto,"P")
//                                 .RuleFor(x=>x.Preco,y=>y.Finance.Random.Decimal())
//                                 .Generate(10);
//             return produtoDTO;
//         }
//     }
// }