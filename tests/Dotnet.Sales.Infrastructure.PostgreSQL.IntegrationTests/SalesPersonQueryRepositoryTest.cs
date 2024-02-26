using AutoMapper;
using AutoMapper.Data;
using Dotnet.Sales.Infrastructure.PostgreSQL.Context;
using Dotnet.Sales.Infrastructure.PostgreSQL.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.IntegrationTests
{
    public sealed class SalesPersonQueryRepositoryTest
    {
        private readonly IOptions<DbContextOptions> _options;
        private readonly IMapper _mapper;

        public SalesPersonQueryRepositoryTest()
        {
            var configuration = TestHelper.GetUserSecret();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.AddProfile<SalesPersonProfile>();
            });
            _options = Options.Create(new DbContextOptions()
            {
                ConnectionString = configuration.GetConnectionString("SalesDb") ?? ""
            });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public async Task GetDistrictAsync_ShouldSingleData()
        {
            using var cts = new CancellationTokenSource();
            var context = new SalesDbContext(_options, _mapper);
            var repository = new SalesPersonQueryRepository(context);
            var result = await repository.GetSalesPersonsAsync([3], cts.Token);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllDistrictAsyncc_ShouldReturnListOfData()
        {
            using var cts = new CancellationTokenSource();
            var context = new SalesDbContext(_options, _mapper);
            var repository = new SalesPersonQueryRepository(context);
            var result = await repository.GetAllSalesPersonsAsync(cts.Token);
            Assert.NotEmpty(result);
        }
    }
}
