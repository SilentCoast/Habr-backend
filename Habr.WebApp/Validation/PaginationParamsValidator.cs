using FluentValidation;
using Habr.Services.Resources;
using Habr.WebApp.Models;

namespace Habr.WebApp.Validation
{
    public class PaginationParamsValidator : AbstractValidator<PaginationParams>
    {
        private const int MinValue = 1;
        private const int MaxPageNumber = 1000;
        private const int MaxPageSize = int.MaxValue;
        public PaginationParamsValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(MinValue)
                .WithMessage(string.Format(ExceptionMessageGeneric.ValueMustBeWithinBounds, nameof(PaginationParams.PageNumber), MinValue, MaxPageNumber));

            RuleFor(x => x.PageSize)
                .GreaterThan(MinValue)
                .LessThanOrEqualTo(MaxPageSize)
                .WithMessage(string.Format(ExceptionMessageGeneric.ValueMustBeWithinBounds, nameof(PaginationParams.PageNumber), MinValue, MaxPageSize));
        }
    }
}
