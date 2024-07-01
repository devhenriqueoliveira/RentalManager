namespace RentalManager.Infrastructure.CrossCutting.Commons.Bases
{
    public class BaseError(string propertyName, string errorMessage)
    {
        public string PropertyName { get; private set; } = propertyName;
        public string ErrorMessage { get; private set; } = errorMessage;
    }
}
