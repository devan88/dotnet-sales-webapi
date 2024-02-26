using AutoMapper;
using Dotnet.Sales.Aggregates;
using System.Data;

namespace Dotnet.Sales.Infrastructure.PostgreSQL.Profiles
{
    internal sealed class SalesPersonProfile : Profile
    {
        public SalesPersonProfile()
        {
            CreateMap<IDataRecord, SalesPerson>();
        }
    }
}
