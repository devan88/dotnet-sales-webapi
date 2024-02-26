namespace Dotnet.Sales.WebApi.ViewModel
{
    public sealed record UpdateDistrictViewModel
    {
        public int DistrictId { get; init; }
        public int PrimarySalesPersonId { get; init; }
        public int[] SecondarySalesPersonIds { get; init; } = Array.Empty<int>();
    }
}
