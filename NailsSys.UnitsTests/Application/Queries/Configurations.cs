using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NailsSys.Application.Configuration;

namespace NailsSys.UnitsTests.Application.Queries
{
    public class Configurations
    {
        public Configurations()
        {
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(new AutoMapperConfigurations()));
            IMapper = mapperConfig.CreateMapper();
        }

        public IMapper IMapper { get; set; }
    }
}