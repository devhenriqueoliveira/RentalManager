
namespace RentalManager.Infrastructure.CrossCutting.Commons.Bases
{
    public class BaseResponse<T>(bool succcess, T data, string message, IEnumerable<BaseError> errors) : BaseResponseGeneric<T>(succcess, data, message, errors)
    {
    }
}
