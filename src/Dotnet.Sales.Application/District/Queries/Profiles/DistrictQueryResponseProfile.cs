using AutoMapper;

namespace Dotnet.Sales.Application.District.Queries.Profiles
{
    internal sealed class DistrictQueryResponseProfile : Profile
    {
        public DistrictQueryResponseProfile()
        {
            CreateMap<Aggregates.District, DistrictQueryResponse>();
        }
    }
}
