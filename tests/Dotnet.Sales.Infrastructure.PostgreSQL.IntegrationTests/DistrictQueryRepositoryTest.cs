using AutoMapper;
using AutoMapper.Data;
using Dotnet.Sales.Infrastructure.PostgreSQL.Context;
using Dotnet.Sales.Infrastructure.PostgreSQL.Profiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.IntegrationTests
{
    public sealed class DistrictQueryRepositoryTest
    {
        private readonly IMapper _mapper;
        private readonly IOptions<DbContextOptions> _options;

        public DistrictQueryRepositoryTest()
        {
            var configuration = TestHelper.GetUserSecret();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddDataReaderMapping();
                cfg.AddProfile<DistrictProfile>();
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
            var repository = new DistrictQueryRepository(context);
            var result = await repository.GetDistrictAsync(3, cts.Token);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllDistrictAsyncc_ShouldReturnListOfData()
        {
            using var cts = new CancellationTokenSource();
            var context = new SalesDbContext(_options, _mapper);
            var repository = new DistrictQueryRepository(context);
            var result = await repository.GetAllDistrictAsync(cts.Token);
            Assert.NotEmpty(result);
        }
    }
}
