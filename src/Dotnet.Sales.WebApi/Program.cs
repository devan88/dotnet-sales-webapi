using Dotnet.Sales.Application;
using Dotnet.Sales.Infrastructure.PostgreSQL;

namespace Dotnet.Sales.WebApi
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddApiVersioning();
            builder.Services.AddCors(builder.Environment, builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddSwagger();
            builder.Host.ConfigureSerilog(builder.Configuration);
            builder.Services.AddExceptionHandler();
            builder.Services.AddApplication();
            builder.Services.AddRepository(builder.Configuration);

            var app = builder.Build();
            app.UseCorrelationIdLogger();
            app.UseExceptionHandler();
            app.UseCors(app.Environment.EnvironmentName);
            app.UseSwagger(app.Environment);


            app.MapControllers();

            app.Run();
        }
    }
}
