// using AutoMapper;
// using NailsSys.Core.Configurations;
// using NailsSys.Core.DTOs;
// using NailsSys.Core.Models;

// namespace NailsSys.Tests
// {    
//     public class AutoMapperConfiguration
//    {
//         private readonly IMapper _mapper;

//         public AutoMapperConfiguration()
//         {
//             var mapperConfig = new MapperConfiguration(x =>
//                 {
//                     x.AddProfile(new AutoMapperConfigurations());
//                 });

//             _mapper = mapperConfig.CreateMapper();
//         }
//         public Produto ObterProdutoMapeado(ProdutoDTO produtoDTO)
//         {            
//             return _mapper.Map<Produto>(produtoDTO);
//         }
//         public IEnumerable<Produto> ObterProdutoMapeado(List<ProdutoDTO> produtoDTO)
//         {            
//             return _mapper.Map<IEnumerable<Produto>>(produtoDTO);
//         }
//     }
// }