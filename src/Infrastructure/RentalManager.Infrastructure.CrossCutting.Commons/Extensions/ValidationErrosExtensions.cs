using FluentValidation.Results;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;

namespace RentalManager.Infrastructure.CrossCutting.Commons.Extensions
{
    public static class ValidationErrosExtensions
    {
        public static IEnumerable<BaseError> MapToBaseErrors(this IEnumerable<ValidationFailure> failures)
        {
            return failures
                .Select(failure => new BaseError(
                    failure.PropertyName,
                    failure.ErrorMessage));
        }
    }
}
