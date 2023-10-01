namespace TesteAccenture.Configuration
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfiguracaoCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("PermitirCredenciais", builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                           .AllowCredentials()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            return services;
        }

        public static IApplicationBuilder ConfiguracaoCors(this IApplicationBuilder app)
        {
            app.UseCors("PermitirCredenciais");

            return app;
        }
    }
}
