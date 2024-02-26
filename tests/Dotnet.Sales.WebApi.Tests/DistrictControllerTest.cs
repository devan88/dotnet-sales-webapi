using Dotnet.Sales.Aggregates;
using Dotnet.Sales.Application.District.Queries;
using Dotnet.Sales.WebApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text;
using System.Text.Json;

namespace Dotnet.Sales.WebApi.Tests
{
    public class DistrictControllerTest
    {
        private readonly Mock<IDistrictQueryRepository> _districtQueryRepositoryMock;
        private readonly Mock<IDistrictRepository> _districtRepositoryMock;
        private readonly Mock<ISalesPersonQueryRepository> _salesPersonQueryRepositoryMock;

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public DistrictControllerTest()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _districtQueryRepositoryMock = new Mock<IDistrictQueryRepository>();
            _districtRepositoryMock = new Mock<IDistrictRepository>();
            _salesPersonQueryRepositoryMock = new Mock<ISalesPersonQueryRepository>();
            var webApplicationFactory = new TestWebApplicationFactory<Program>
            {
                ConfigureMockServices = (services) =>
                {
                    services.AddScoped(sp => _districtQueryRepositoryMock.Object);
                    services.AddScoped(sp => _districtRepositoryMock.Object);
                    services.AddScoped(sp => _salesPersonQueryRepositoryMock.Object);
                }
            };
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAllDistrictsV1_WhenRequestIsValid_ShouldReturnAllDistrictData()
        {
            //Arrange
            var districts = new List<District>() {
                new District(1, "test", 1, [1], ["store"])
            };
            _districtQueryRepositoryMock
                .Setup(m => m.GetAllDistrictAsync(default))
                .ReturnsAsync(districts);

            //Act
            var response = await _httpClient.GetAsync("/api/v1/district");
            var result = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<IEnumerable<DistrictQueryListResponse>>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(districts[0].Name, responseModel?.First().Name);
        }

        [Fact]
        public async Task GetDistrictsV1_WhenRequestIsValid_ShouldReturnDistrictData()
        {
            //Arrange
            var district = new District(1, "test", 1, [1], ["store"]);
            _districtQueryRepositoryMock
                .Setup(m => m.GetDistrictAsync(It.IsAny<int>(), default))
                .ReturnsAsync(district);

            //Act
            var response = await _httpClient.GetAsync("/api/v1/district/1");
            var result = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<DistrictQueryResponse>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(district.Name, responseModel?.Name);
        }

        [Fact]
        public async Task UpdateDistrictV1_WhenRequestIsValid_ShouldReturnNoContentResult()
        {
            //Arrange
            var salesPersons = new List<SalesPerson>() { new SalesPerson(1, "SP-A"), new SalesPerson(2, "SP-B") };
            var request = new UpdateDistrictViewModel { DistrictId = 1, PrimarySalesPersonId = 1, SecondarySalesPersonIds = [2] };
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            _districtRepositoryMock
                .Setup(m => m.UpdateSalesPersonAsync(It.IsAny<District>(), default))
                .ReturnsAsync(true);
            var district = new District(1, "test", 1, [1], ["store"]);
            _districtQueryRepositoryMock
                .Setup(m => m.GetDistrictAsync(It.IsAny<int>(), default))
                .ReturnsAsync(district);
            _salesPersonQueryRepositoryMock
                .Setup(m => m.GetAllSalesPersonsAsync(default))
                .ReturnsAsync(salesPersons);

            //Act
            var response = await _httpClient.PutAsync("/api/v1/district/1", stringContent);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task UpdateDistrictV1_WhenSalesPersonDoesNotExist_ShouldReturnNoBadRequest()
        {
            //Arrange
            var salesPersons = new List<SalesPerson>() { new SalesPerson(1, "SP-A") };
            var request = new UpdateDistrictViewModel { DistrictId = 1, PrimarySalesPersonId = 1, SecondarySalesPersonIds = [2] };
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            _salesPersonQueryRepositoryMock
                .Setup(m => m.GetAllSalesPersonsAsync(default))
                .ReturnsAsync(salesPersons);

            //Act
            var response = await _httpClient.PutAsync("/api/v1/district/1", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(400, problemDetails?.Status);
        }

        [Fact]
        public async Task UpdateDistrictV1_WhenDistrictDoesNotExist_ShouldReturnInternalServerError()
        {
            //Arrange
            var salesPersons = new List<SalesPerson>() { new SalesPerson(1, "SP-A"), new SalesPerson(2, "SP-B") };
            var request = new UpdateDistrictViewModel { DistrictId = 1, PrimarySalesPersonId = 1, SecondarySalesPersonIds = [2] };
            var stringContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            _salesPersonQueryRepositoryMock
                .Setup(m => m.GetAllSalesPersonsAsync(default))
                .ReturnsAsync(salesPersons);

            //Act
            var response = await _httpClient.PutAsync("/api/v1/district/1", stringContent);
            var result = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
            Assert.Equal(500, problemDetails?.Status);
        }
    }
}