using Dotnet.Sales.Application.District.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnet.Sales.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            return services.AddValidatorsFromAssemblyContaining<UpdateDistrictCommandValidator>();
        }
    }
}
