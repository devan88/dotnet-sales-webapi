using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales.Infrastructure.PostgreSQL
{
    public sealed class DistrictQueryRepository : IDistrictQueryRepository
    {
        private readonly IDbContext _dbContext;

        public DistrictQueryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "argument {arg} is required");
        }

        public async Task<IEnumerable<District>> GetAllDistrictAsync(CancellationToken cancellationToken)
        {
            var commandText = @"SELECT id, name, primary_sales_person_id, secondary_sales_person_ids, stores FROM public.district ORDER BY id";
            var districts = await _dbContext.QueryModelAsync<District>(commandText, cancellationToken: cancellationToken);
            return districts ?? Enumerable.Empty<District>();
        }

        public async Task<District> GetDistrictAsync(int id, CancellationToken cancellationToken)
        {
            var commandText = @"SELECT id, name, primary_sales_person_id, secondary_sales_person_ids, stores FROM public.district WHERE id = @id ORDER BY id";
            var parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };
            var district = await _dbContext.QueryModelAsync<District>(commandText, parameters, cancellationToken: cancellationToken);
            return district.FirstOrDefault() ?? District.Empty();
        }
    }
}
