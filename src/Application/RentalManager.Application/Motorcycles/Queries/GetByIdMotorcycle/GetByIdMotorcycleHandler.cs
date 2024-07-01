using MediatR;
using RentalManager.Domain.Entities;
using RentalManager.Infrastructure.Data.UnitOfWork;

namespace RentalManager.Application.Motorcycles.Queries.GetByIdMotorcycle
{
    public class GetByIdMotorcycleHandler(
        IUnitOfWork unitOfWork) : 
        IRequestHandler<GetByIdMotorcycleQuery, Motorcycle>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Motorcycle> Handle(GetByIdMotorcycleQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Motorcycles.GetByIdAsync(request.Id);
        }
    }
}
