using FluentValidation;
using MediatR;
using RentalManager.Domain.Entities;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;
using RentalManager.Infrastructure.Data.UnitOfWork;

namespace RentalManager.Application.Motorcycles.Commands.UpdateMotorcycle
{
    public class UpdateMotorcycleHandler(
        IUnitOfWork unitOfWork,
        IValidator<UpdateMotorcycleCommand> validator) : 
        IRequestHandler<UpdateMotorcycleCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<UpdateMotorcycleCommand> _validator = validator;

        public async Task<BaseResponse<bool>> Handle(UpdateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return await BaseResult.WithFailures<bool>(validationResult.Errors);

            return await CreateAndCommitInDatabaseAsync(request);
        }

        #region Private Methods
        private async Task<BaseResponse<bool>> CreateAndCommitInDatabaseAsync(UpdateMotorcycleCommand request)
        {
            var motorcycle = new Motorcycle();

            motorcycle.PersisteId(request.Id);
            motorcycle.ChangeYear(request.Year);
            motorcycle.ChangePlate(request.Plate);
            motorcycle.ChangeModel(request.Model);

            var resultCreateMotorcycle = await _unitOfWork.Motorcycles.UpdateAsync(motorcycle);

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
