using Microsoft.OpenApi.Models;
using System.Net.NetworkInformation;

namespace TesteAccenture.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Teste Accenture DEV",
                    Description = "Teste para dev da Accenture. CRUD simples em .NET 7.",
                    Contact = new OpenApiContact { Name = "Walter de Camargo", Email = "walter.camargo.gr@gmail.com" }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
