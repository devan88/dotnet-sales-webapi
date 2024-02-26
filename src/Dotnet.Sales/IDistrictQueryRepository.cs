using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales
{
    public interface IDistrictQueryRepository
    {
        Task<IEnumerable<District>> GetAllDistrictAsync(CancellationToken cancellationToken);

        Task<District> GetDistrictAsync(int id, CancellationToken cancellationToken);
    }
}
