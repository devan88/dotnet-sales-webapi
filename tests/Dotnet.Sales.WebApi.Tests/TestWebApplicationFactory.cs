using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Sales.WebApi.Tests
{
    internal class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
        where TProgram : class
    {
        public Action<IServiceCollection>? ConfigureMockServices { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services => ConfigureMockServices?.Invoke(services));
            builder.UseEnvironment("Development");
        }
    }
}
