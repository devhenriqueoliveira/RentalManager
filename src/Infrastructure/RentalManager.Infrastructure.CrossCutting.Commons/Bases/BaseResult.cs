using FluentValidation.Results;
using RentalManager.Infrastructure.CrossCutting.Commons.Constants;
using RentalManager.Infrastructure.CrossCutting.Commons.Extensions;

namespace RentalManager.Infrastructure.CrossCutting.Commons.Bases
{
    public class BaseResult
    {
        public static async Task<BaseResponse<T>> WithFailures<T>(IEnumerable<ValidationFailure> failures, T result = default!) 
            => await Task.FromResult(new BaseResponse<T>(false, result, ResultConstants.ERROR_MESSAGE, failures.MapToBaseErrors()));

        public static async Task<BaseResponse<T>> WithSuccess<T>(T result)
            => await Task.FromResult(new BaseResponse<T>(true, result, ResultConstants.SUCCESS_MESSAGE, default!));

        public static async Task<BaseResponse<T>> WithErrors<T>(string message, T result = default!)
            => await Task.FromResult(new BaseResponse<T>(false, result, message, default!));
    }
}
