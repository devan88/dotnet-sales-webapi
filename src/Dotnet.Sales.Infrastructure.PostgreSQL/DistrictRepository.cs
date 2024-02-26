using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales.Infrastructure.PostgreSQL
{
    public sealed class DistrictRepository : IDistrictRepository
    {
        private readonly IDbContext _dbContext;

        public DistrictRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "argument {arg} is required");
        }

        public async Task<bool> UpdateSalesPersonAsync(District district, CancellationToken cancellationToken)
        {
            var commandText = @"
                UPDATE public.district
                SET primary_sales_person_id = @primarySalesPersonId,
                    secondary_sales_person_ids = @secondarySalesPersonIds
                WHERE id = @districtId";
            var parameters = new Dictionary<string, object>
            {
                { "@primarySalesPersonId", district.PrimarySalesPersonId },
                { "@secondarySalesPersonIds", district.SecondarySalesPersonIds },
                { "@districtId", district.Id }
            };
            var result = await _dbContext.ExecuteModelAsync(commandText, parameters, cancellationToken: cancellationToken);
            return result > 0;
        }
    }
}
