namespace RentalManager.Infrastructure.CrossCutting.Commons.Bases
{
    public class BaseResponseGeneric<T>(bool succcess, T data, string message, IEnumerable<BaseError> errors)
    {
        public bool Succcess { get; set; } = succcess;
        public T Data { get; set; } = data;
        public string Message { get; set; } = message;
        public IEnumerable<BaseError> Errors { get; set; } = errors;
    }
}
