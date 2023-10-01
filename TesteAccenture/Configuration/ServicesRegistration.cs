using Microsoft.Extensions.DependencyInjection;
using TesteAccenture.Repositories;

namespace TesteAccenture.Services
{
    public static class ServicesRegistration
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddHttpClient<IValidationsService, ValidationsService>();
            services.AddScoped<IValidationRepository, ValidationRepository>();

            return services;
        }
    }
}
