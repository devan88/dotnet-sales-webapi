using AutoMapper;

namespace Dotnet.Sales.Application.District.Queries.Profiles
{
    internal sealed class DistrictQueryListResponseProfile : Profile
    {
        public DistrictQueryListResponseProfile()
        {
            CreateMap<Aggregates.District, DistrictQueryListResponse>();
        }
    }
}
