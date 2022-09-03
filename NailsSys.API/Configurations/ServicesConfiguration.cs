using FluentValidation;
using FluentValidation.AspNetCore;
using NailsSys.Application.Validations;

namespace NailsSys.API.Configurations
{
    public static class ServicesConfiguration
    {
        public static void AddServicesConfigurations(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
                .Where(p => !p.IsDynamic));

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<AlterarAgendamentoCommandValidation>();
        }
    }
}