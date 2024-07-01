using FluentValidation;
using MediatR;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;
using RentalManager.Infrastructure.Data.UnitOfWork;

namespace RentalManager.Application.Motorcycles.Commands.DeleteMotorcycle
{
    public class DeleteMotorcycleHandler(
        IUnitOfWork unitOfWork,
        IValidator<DeleteMotorcycleCommand> validator) : 
        IRequestHandler<DeleteMotorcycleCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<DeleteMotorcycleCommand> _validator = validator;

        public async Task<BaseResponse<bool>> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return await BaseResult.WithFailures<bool>(validationResult.Errors);

            return await CreateAndCommitInDatabaseAsync(request);
        }

        #region Private Methods
        private async Task<BaseResponse<bool>> CreateAndCommitInDatabaseAsync(DeleteMotorcycleCommand request)
        {
            var resultCreateMotorcycle = await _unitOfWork.Motorcycles.DeleteAsync(request.Id);

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
