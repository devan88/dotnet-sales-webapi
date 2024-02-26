using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.IntegrationTests
{
    internal static class TestHelper
    {
        public static IConfiguration GetUserSecret()
        {
            return new ConfigurationBuilder()
                .AddUserSecrets(Assembly.GetExecutingAssembly())
                .Build();
        }
    }
}
