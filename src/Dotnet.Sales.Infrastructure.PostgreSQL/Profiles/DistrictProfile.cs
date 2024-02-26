using AutoMapper;
using Dotnet.Sales.Aggregates;
using System.Data;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.Profiles
{
    internal sealed class DistrictProfile : Profile
    {
        public DistrictProfile()
        {
            CreateMap<IDataRecord, District>()
                .ForCtorParam(
                    nameof(District.PrimarySalesPersonId),
                    opt => opt.MapFrom(src => src.GetValue(src.GetOrdinal("primary_sales_person_id"))))
                .ForCtorParam(
                    nameof(District.SecondarySalesPersonIds),
                    opt => opt.MapFrom(src => src.GetValue(src.GetOrdinal("secondary_sales_person_ids"))));
        }
    }
}
