namespace Dotnet.Sales.Aggregates
{
    public sealed class SalesPerson
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        private SalesPerson() { }

        public SalesPerson(int id, string name)
        {
            Id = id;
            Name = name;
            ValidateSalesPerson(this);
        }

        public static SalesPerson Empty()
        {
            return new SalesPerson()
            {
                Name = string.Empty
            };
        }

        private static void ValidateSalesPerson(SalesPerson salesPerson)
        {
            if (salesPerson.Id <= 0)
            {
                throw new DomainException($"SalesPerson {nameof(Id)} must be positive integer");
            }
            if (string.IsNullOrEmpty(salesPerson.Name))
            {
                throw new DomainException($"SalesPerson {nameof(Name)} must not be empty");
            }
        }
    }
}
