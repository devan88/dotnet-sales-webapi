using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales.Infrastructure.PostgreSQL
{
    public sealed class SalesPersonQueryRepository : ISalesPersonQueryRepository
    {
        private readonly IDbContext _dbContext;

        public SalesPersonQueryRepository(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "{arg} is required");
        }

        public async Task<IEnumerable<SalesPerson>> GetAllSalesPersonsAsync(CancellationToken cancellationToken)
        {
            var commandText = @"SELECT id, name FROM public.sales_person ORDER BY id";
            var salesPersons = await _dbContext.QueryModelAsync<SalesPerson>(commandText, cancellationToken: cancellationToken);
            return salesPersons ?? Enumerable.Empty<SalesPerson>();
        }

        public async Task<IEnumerable<SalesPerson>> GetSalesPersonsAsync(int[] ids, CancellationToken cancellationToken)
        {
            var commandText = @"SELECT id, name FROM public.sales_person WHERE id = ANY (@ids) ORDER BY id";
            var parameters = new Dictionary<string, object>
            {
                { "@ids", ids }
            };
            var salesPersons = await _dbContext.QueryModelAsync<SalesPerson>(commandText, parameters, cancellationToken: cancellationToken);
            return salesPersons ?? Enumerable.Empty<SalesPerson>();
        }
    }
}
