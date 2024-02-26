using AutoFixture.Xunit2;
using Dotnet.Sales.Aggregates;
using Dotnet.Sales.Application.SalesPerson.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text.Json;

namespace Dotnet.Sales.WebApi.Tests
{
    public class SalesPersonControllerTest
    {
        private readonly Mock<ISalesPersonQueryRepository> _salesPersonQueryRepositoryMock;

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public SalesPersonControllerTest()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            _salesPersonQueryRepositoryMock = new Mock<ISalesPersonQueryRepository>();
            var webApplicationFactory = new TestWebApplicationFactory<Program>
            {
                ConfigureMockServices = (services) =>
                {
                    services.AddScoped(sp => _salesPersonQueryRepositoryMock.Object);
                }
            };
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task GetAllSalesPersonsV1_WhenRequestIsValid_ShouldReturnAllSalesPersonData()
        {
            //Arrange
            var salesPersons = new List<SalesPerson>() {
                new SalesPerson(1, "test"), 
                new SalesPerson(2, "test")
            };
            _salesPersonQueryRepositoryMock
                .Setup(m => m.GetAllSalesPersonsAsync(default))
                .ReturnsAsync(salesPersons);

            //Act
            var response = await _httpClient.GetAsync("/api/v1/salesperson");
            var result = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<IEnumerable<SalesPersonQueryListResponse>>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(salesPersons[0].Name, responseModel?.First().Name);
        }

        [Fact]
        public async Task GetSalesPersonsV1_WhenRequestIsValid_ShouldReturnSalesPersonsData()
        {
            //Arrange
            var salesPersons = new List<SalesPerson>() {
                new SalesPerson(1, "test-1"),
                new SalesPerson(2, "test-2")
            };
            _salesPersonQueryRepositoryMock
                .Setup(m => m.GetSalesPersonsAsync(It.IsAny<int[]>(), default))
                .ReturnsAsync(salesPersons);
            var queryParameters = new Dictionary<string, string>
            {
                { "salesPersonIds[0]", "1" },
                { "salesPersonIds[1]", "2"}
            };
            var dictFormUrlEncoded = new FormUrlEncodedContent(queryParameters);
            var queryString = await dictFormUrlEncoded.ReadAsStringAsync();

            //Act
            var response = await _httpClient.GetAsync($"/api/v1/salesperson/ids?{queryString}");
            var result = await response.Content.ReadAsStringAsync();
            var responseModel = JsonSerializer.Deserialize<IEnumerable<SalesPersonQueryListResponse>>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(salesPersons[0].Name, responseModel?.First().Name);
        }

        [Theory]
        [InlineAutoData("")]
        [InlineAutoData("salesPersonIds[0]=0&salesPersonIds[1]=0")]
        public async Task GetSalesPersonsV1_WhenRequestIsNotValid_ShouldReturnNoBadRequest(string queryString)
        {
            //Act
            var response = await _httpClient.GetAsync($"/api/v1/salesperson/ids?{queryString}");
            var result = await response.Content.ReadAsStringAsync();
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(result, _jsonSerializerOptions);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(400, problemDetails?.Status);
        }
    }
}