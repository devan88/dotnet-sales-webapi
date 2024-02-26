using AutoMapper;
using Dotnet.Sales.Aggregates;
using Dotnet.Sales.Infrastructure.PostgreSQL.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Data.Common;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.IntegrationTests
{
    public sealed class DistrictRepositoryTest
    {
        private readonly IOptions<DbContextOptions> _options;
        private readonly Mock<IMapper> _mapper;

        public DistrictRepositoryTest()
        {
            var configuration = TestHelper.GetUserSecret();
            _options = Options.Create(new DbContextOptions()
            {
                ConnectionString = configuration.GetConnectionString("SalesDb") ?? ""
            });
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public async Task UpdateDistrictSalesPersonAsync_ShouldUpdateData()
        {
            using var cts = new CancellationTokenSource();
            var context = new SalesDbContext(_options, _mapper.Object);
            var repository = new DistrictRepository(context);
            var district = new District(1, "Name", 1, [2, 3, 4, 5, 6], []);
            var isUpdated = await repository.UpdateSalesPersonAsync(district, cts.Token);
            Assert.True(isUpdated);
        }

        [Fact]
        public async Task UpdateDistrictSalesPersonAsync_WhenSecondarySalesPersonDoesNotExist_ShouldThrowDbException()
        {
            using var cts = new CancellationTokenSource();
            var context = new SalesDbContext(_options, _mapper.Object);
            var repository = new DistrictRepository(context);
            var district = new District(1, "Name", 1, [12, 3, 4, 5, 6], []);
            var updateSalesPersonAsync = () => repository.UpdateSalesPersonAsync(district, cts.Token);
            await Assert.ThrowsAnyAsync<DbException>(updateSalesPersonAsync);
        }
    }
}
