using AutoMapper;
using Microsoft.Extensions.Options;
using Npgsql;
using System.Data;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace Dotnet.Sales.Infrastructure.PostgreSQL.Context
{
    internal sealed class SalesDbContext : IDbContext
    {
        private readonly DbContextOptions _dbContextOptions;
        private readonly IMapper _mapper;

        public SalesDbContext(
            IOptions<DbContextOptions> options,
            IMapper mapper)
        {
            _dbContextOptions = options.Value;
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> QueryModelAsync<T>(
            string commandText,
            IDictionary<string, object>? parameters = default,
            CancellationToken cancellationToken = default)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_dbContextOptions.ConnectionString);
            await using var dataSource = dataSourceBuilder.Build();
            await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
            await using var command = new NpgsqlCommand(commandText, connection);
            if (parameters is not null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            if (reader.HasRows)
            {
                return _mapper.Map<IDataReader, IEnumerable<T>>(reader) ?? Enumerable.Empty<T>();
            }
            return Enumerable.Empty<T>();
        }

        public async Task<int> ExecuteModelAsync(
            string commandText,
            IDictionary<string, object>? parameters = default,
            CancellationToken cancellationToken = default)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_dbContextOptions.ConnectionString);
            await using var dataSource = dataSourceBuilder.Build();
            await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);
            await using var command = new NpgsqlCommand(commandText, connection);
            if (parameters is not null && parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
            }
            return await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
