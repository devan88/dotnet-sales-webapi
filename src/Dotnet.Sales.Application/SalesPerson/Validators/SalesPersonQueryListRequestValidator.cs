using Dotnet.Sales.Aggregates;
using Dotnet.Sales.Application.SalesPerson.Queries;
using FluentValidation;

namespace Dotnet.Sales.Application.SalesPerson.Validators
{
    public sealed class SalesPersonQueryListRequestValidator : AbstractValidator<SalesPersonQueryListRequest>
    {
        public SalesPersonQueryListRequestValidator()
        {
            RuleFor(query => query.Ids)
                .NotEmpty()
                .Must(ids => Array.TrueForAll(ids, id => id > 0))
                .WithMessage("The ids must not be empty and the values must be positive integer");
        }
    }
}
