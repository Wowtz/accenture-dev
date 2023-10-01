using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TesteAccenture.Models;

namespace TesteAccenture.Configuration
{
    public static class EntityFrameworkConfiguration
    {
        public static IServiceCollection ConfigureEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Obtém a string de conexão do arquivo appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configura o DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString)
            );

            return services;
        }
    }
}
