namespace Dotnet.Sales
{
    public interface IDbContext
    {
        Task<IEnumerable<T>> QueryModelAsync<T>(
            string commandText,
            IDictionary<string, object>? parameters = default,
            CancellationToken cancellationToken = default);

        Task<int> ExecuteModelAsync(
            string commandText,
            IDictionary<string, object>? parameters = default,
            CancellationToken cancellationToken = default);
    }
}
