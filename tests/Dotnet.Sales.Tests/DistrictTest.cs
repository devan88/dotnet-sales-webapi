using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales.Tests
{
    public class DistrictTest
    {

        [Theory]
        [MemberData(nameof(Districts))]
        public void NewDistrict_WhenDistrictParameterIsInvalid_ShouldThrowDomainException(
            int id,
            string name,
            int primarySalesPersonId,
            IEnumerable<int> secondarySalesPersonIds,
            IEnumerable<string> stores)
        {
            var districtAction = () => new District(id, name, primarySalesPersonId, secondarySalesPersonIds, stores);
            Assert.Throws<DomainException>(districtAction);
        }

        [Fact]
        public void UpdateSalesPerson_WhenPrimarySalesPersonIsInSecondarySalesPerson_ShouldThrowInvalidDataException()
        {
            var district = District.Empty();
            var updateSalesPerson = () => district.UpdateSalesPerson(1, [1,2]);
            Assert.Throws<InvalidDataException>(updateSalesPerson);
        }

        public static IEnumerable<object[]> Districts =>
        [
            [0, "Name", 1, new List<int>() { 1 }, Enumerable.Empty<string>()],
            [1, "", 1, new List<int>() { 1 }, Enumerable.Empty<string>()],
            [1, "Name", 0, new List<int>() { 1 }, Enumerable.Empty<string>()],
            [1, "Name", 1,  Enumerable.Empty<int>(), Enumerable.Empty<string>()],
            [1, "Name", 1, new List<int>() { 0 }, Enumerable.Empty<string>()],
        ];
    }
}
