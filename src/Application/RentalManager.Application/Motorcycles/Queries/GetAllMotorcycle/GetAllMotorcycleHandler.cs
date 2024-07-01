using MediatR;
using RentalManager.Domain.Entities;
using RentalManager.Infrastructure.Data.UnitOfWork;

namespace RentalManager.Application.Motorcycles.Queries.GetAllMotorcycle
{
    public class GetAllMotorcycleHandler(
        IUnitOfWork unitOfWork) : 
        IRequestHandler<GetAllMotorcycleQuery, IReadOnlyCollection<Motorcycle>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IReadOnlyCollection<Motorcycle>> Handle(GetAllMotorcycleQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.Motorcycles.GetAllAsync();
        }
    }
}
