using Dotnet.Sales.Application.District.Queries;
using FluentValidation;

namespace Dotnet.Sales.Application.District.Validators
{
    public sealed class DistrictQueryRequestValidator : AbstractValidator<DistrictQueryRequest>
    {
        public DistrictQueryRequestValidator()
        {
            RuleFor(query => query.DistrictId)
                .NotEmpty()
                .WithMessage("The district id can't be empty.");

        }
    }
}
