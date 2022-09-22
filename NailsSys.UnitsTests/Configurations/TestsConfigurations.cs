using AutoMapper;
using FluentValidation.Results;
using NailsSys.Application.Configuration;

namespace NailsSys.UnitsTests.Application.Configurations
{
    public class TestsConfigurations
    {
        public TestsConfigurations()
        {
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(new AutoMapperConfigurations()));
            IMapper = mapperConfig.CreateMapper();
        }

        public IMapper IMapper { get; set; }

        protected List<string> ObterListagemErro(ValidationResult result)
        {
            return result.Errors.Select(x => x.ErrorMessage).ToList();
        }
    }
}