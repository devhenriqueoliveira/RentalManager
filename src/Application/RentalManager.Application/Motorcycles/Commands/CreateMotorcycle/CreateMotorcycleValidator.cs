using FluentValidation;
using RentalManager.Infrastructure.Data.Interfaces;

namespace RentalManager.Application.Motorcycles.Commands.CreateMotorcycle
{
    public class CreateMotorcycleValidator : AbstractValidator<CreateMotorcycleCommand>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleValidator(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;

            RuleFor(command => command.Year)
                .NotEmpty().WithMessage("Plate cannot be empty.")
                .NotNull().WithMessage("Plate cannot be null.")
                .GreaterThan(0).WithMessage("Plate must be greater than 0.")
                .ExclusiveBetween(1885, DateTime.Now.Year).WithMessage($"Plate must be between 1885 and {DateTime.Now.Year}");
            
            RuleFor(command => command.Plate)
                .NotEmpty().WithMessage("Plate cannot be empty.")
                .NotNull().WithMessage("Plate cannot be null.")
                .MustAsync(BeUniquePlate).WithMessage("Plate must be unique.");

            RuleFor(command => command.Model)
                .NotEmpty().WithMessage("Plate cannot be empty.")
                .NotNull().WithMessage("Plate cannot be null.");
        }

        private async Task<bool> BeUniquePlate(string plate, CancellationToken cancellationToken)
        {
            var exists = await _motorcycleRepository.ExistsByPlateAsync(plate);
            return !exists;
        }
    }
}
