using AutoMapper.Data;
using Dotnet.Sales.Infrastructure.PostgreSQL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnet.Sales.Infrastructure.PostgreSQL
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SalesDb")
                ?? throw new MissingFieldException("ConnectionStrings:SalesDb is required");

            return services
                .Configure<DbContextOptions>(opts => opts.ConnectionString = connectionString)
                .AddAutoMapper(cfg =>
                {
                    cfg.AddDataReaderMapping();
                }, Assembly.GetExecutingAssembly())
                .AddScoped<IDbContext, SalesDbContext>()
                .AddScoped<IDistrictQueryRepository, DistrictQueryRepository>()
                .AddScoped<IDistrictRepository, DistrictRepository>()
                .AddScoped<ISalesPersonQueryRepository, SalesPersonQueryRepository>();
        }
    }
}
