using AutoMapper;

namespace Dotnet.Sales.Application.SalesPerson.Queries.Profiles
{
    internal sealed class SalesPersonQueryListResponseProfile : Profile
    {
        public SalesPersonQueryListResponseProfile()
        {
            CreateMap<Aggregates.SalesPerson, SalesPersonQueryListResponse>();
        }
    }
}
