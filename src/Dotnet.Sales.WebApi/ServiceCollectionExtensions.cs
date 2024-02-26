using Asp.Versioning;
using Dotnet.Sales.WebApi.Configurations;
using Dotnet.Sales.WebApi.Diagnostics;
using Microsoft.OpenApi.Models;

namespace Dotnet.Sales.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new UrlSegmentApiVersionReader(),
                    new HeaderApiVersionReader("X-Api-Version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });
            return services;
        }

        public static IServiceCollection AddCors(this IServiceCollection services, IHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            var corsPolicy = configuration.GetSection($"Cors:{hostEnvironment.EnvironmentName}").Get<CorsPolicy>();
            if (corsPolicy is not null)
            {
                return services.AddCors(options =>
                {
                    options.AddPolicy(name: $"{hostEnvironment.EnvironmentName}",
                        policy =>
                        {
                            policy.WithOrigins(corsPolicy.Origins);
                            policy.AllowAnyHeader();
                            policy.AllowAnyMethod();
                        });
                });
            }
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dotnet.Sales.WebApi - V1", Version = "v1" });
                    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Dotnet.Sales.WebApi - V2", Version = "v2" });
                });
        }

        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
            return services
                .AddProblemDetails()
                .AddExceptionHandler<ValidationExceptionHandler>();
        }
    }
}
