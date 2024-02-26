using Dotnet.Sales.Aggregates;

namespace Dotnet.Sales.Tests
{
    public class SalesPersonTest
    {

        [Theory]
        [MemberData(nameof(SalesPersons))]
        public void NewSalesPersons_WhenSalesPersonsParameterIsInvalid_ShouldThrowDomainException(
            int id,
            string name)
        {
            var salesPersonsAction = () => new SalesPerson(id, name);
            Assert.Throws<DomainException>(salesPersonsAction);
        }

        public static IEnumerable<object[]> SalesPersons =>
        [
            [0, "Name"],
            [1, ""],
        ];
    }
}
