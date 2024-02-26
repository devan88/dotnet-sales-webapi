using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales
{
    public interface ISalesPersonQueryRepository
    {
        Task<IEnumerable<SalesPerson>> GetAllSalesPersonsAsync(CancellationToken cancellationToken);
        Task<IEnumerable<SalesPerson>> GetSalesPersonsAsync(int[] ids, CancellationToken cancellationToken);
    }
}
