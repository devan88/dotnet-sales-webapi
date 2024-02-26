using Dotnet.Sales.Application.District.Commands;
using FluentValidation;

namespace Dotnet.Sales.Application.District.Validators
{
    public sealed class UpdateDistrictCommandValidator : AbstractValidator<UpdateDistrictCommandRequest>
    {
        private readonly ISalesPersonQueryRepository _salesPersonQueryRepository;

        public UpdateDistrictCommandValidator(ISalesPersonQueryRepository salesPersonQueryRepository)
        {
            _salesPersonQueryRepository = salesPersonQueryRepository;

            RuleFor(command => command.DistrictId)
                .NotEmpty()
                .WithMessage("The district id can't be empty.");
            RuleFor(command => command.PrimarySalesPersonId)
                .Must(id => id is not 0)
                .WithMessage("PrimarySalesPersonId must be positive integer");
            RuleFor(command => command.SecondarySalesPersonIds)
                .Must(ids => ids.Length > 0 && Array.TrueForAll(ids, id => id > 0))
                .WithMessage("SecondarySalesPersonIds must not be empty and the values must be positive integer");
            RuleFor(command => command)
                .Must(command => !command.SecondarySalesPersonIds.Contains(command.PrimarySalesPersonId))
                .WithMessage("Sales Person cannot be both as primary and secondary");
            RuleFor(command => command).MustAsync(VerifySalesPersonExistsAsync)
                .WithMessage("PrimarySalesPersonId and/or SecondarySalesPersonIds does not exists in database");

        }

        private async Task<bool> VerifySalesPersonExistsAsync(
            UpdateDistrictCommandRequest command,
            CancellationToken cancellationToken)
        {
            var salesPersons = (await _salesPersonQueryRepository.GetAllSalesPersonsAsync(cancellationToken)).Select(sp => sp.Id);
            var isValidPrimary = salesPersons.Contains(command.PrimarySalesPersonId);
            var isValidSecondary = Array.TrueForAll(command.SecondarySalesPersonIds, id => salesPersons.Contains(id));
            return isValidPrimary && isValidSecondary;
        }
    }
}
