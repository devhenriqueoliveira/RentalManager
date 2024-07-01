using FluentValidation;
using MediatR;
using RentalManager.Domain.Entities;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;
using RentalManager.Infrastructure.Data.UnitOfWork;

namespace RentalManager.Application.Motorcycles.Commands.CreateMotorcycle
{
    public class CreateMotorcycleHandler(
        IUnitOfWork unitOfWork, 
        IValidator<CreateMotorcycleCommand> validator) : IRequestHandler<CreateMotorcycleCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<CreateMotorcycleCommand> _validator = validator;

        public async Task<BaseResponse<bool>> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return await BaseResult.WithFailures<bool>(validationResult.Errors);
            
            return await CreateAndCommitInDatabaseAsync(request);
        }

        #region Private Methods
        private async Task<BaseResponse<bool>> CreateAndCommitInDatabaseAsync(CreateMotorcycleCommand request)
        {
            var resultCreateMotorcycle = await _unitOfWork.Motorcycles.CreateAsync(new Motorcycle(request.Year, request.Plate, request.Model));

            if (!resultCreateMotorcycle)
                return await BaseResult.WithErrors<bool>("Não foi possível salvar no banco de dados");

            var resultCommit = await _unitOfWork.CommitAsync();

            if (!resultCommit)
                return await BaseResult.WithErrors<bool>("Não foi possível salvar no banco de dados");

            return await BaseResult.WithSuccess(true);
        }

        #endregion
    }
}
