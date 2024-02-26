namespace Dotnet.Sales.Aggregates
{
    public sealed class District
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public int PrimarySalesPersonId { get; private set; }
        public IEnumerable<int> SecondarySalesPersonIds { get; private set; } = Enumerable.Empty<int>();
        public IEnumerable<string> Stores { get; private set; } = Enumerable.Empty<string>();

        private District() { }

        public District(
            int id,
            string name,
            int primarySalesPersonId,
            IEnumerable<int> secondarySalesPersonIds,
            IEnumerable<string> stores)
        {
            Id = id;
            Name = name;
            PrimarySalesPersonId = primarySalesPersonId;
            SecondarySalesPersonIds = secondarySalesPersonIds;
            Stores = stores;
            ValidateDistrict(this);
        }

        public static District Empty()
        {
            return new District();
        }

        public void UpdateSalesPerson(int primarySalesPersonId, IEnumerable<int> secondarySalesPersonIds)
        {
            if (secondarySalesPersonIds.Contains(primarySalesPersonId))
            {
                throw new InvalidDataException("Sales Person cannot be both as primary and secondary");
            }
            PrimarySalesPersonId = primarySalesPersonId;
            SecondarySalesPersonIds = secondarySalesPersonIds;
        }

        private static void ValidateDistrict(District district)
        {
            if (district.Id <= 0)
            {
                throw new DomainException($"District {nameof(Id)} must be positive integer");
            }
            if (string.IsNullOrEmpty(district.Name))
            {
                throw new DomainException($"District {nameof(Name)} must not be empty");
            }
            if (district.PrimarySalesPersonId <= 0)
            {
                throw new DomainException($"District {nameof(PrimarySalesPersonId)} must be positive integer");
            }
            if (!district.SecondarySalesPersonIds.Any()
                || !district.SecondarySalesPersonIds.All(id => id > 0))
            {
                throw new DomainException($"District {nameof(SecondarySalesPersonIds)} must not be empty and the values must be positive integer");
            }
        }
    }
}
