namespace Dotnet.Sales.Infrastructure.PostgreSQL.Context
{
    public sealed class DbContextOptions
    {
        public required string ConnectionString { get; set; }
    }
}