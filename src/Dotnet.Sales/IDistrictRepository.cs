using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales
{
    public interface IDistrictRepository
    {
        Task<bool> UpdateSalesPersonAsync(District district, CancellationToken cancellationToken);
    }
}
