using FluentValidation;

namespace RentalManager.Application.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleValidator : AbstractValidator<DeleteMotorcycleCommand>
    {
        public DeleteMotorcycleValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty().WithMessage("Plate cannot be empty.")
                .NotNull().WithMessage("Plate cannot be null.");
        }
    }
}
