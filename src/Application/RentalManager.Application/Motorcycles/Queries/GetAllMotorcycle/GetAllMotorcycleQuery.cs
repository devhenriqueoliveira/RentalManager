using MediatR;
using RentalManager.Domain.Entities;

namespace RentalManager.Application.Motorcycles.Queries.GetAllMotorcycle
{
    public record GetAllMotorcycleQuery : IRequest<IReadOnlyCollection<Motorcycle>>;
}
