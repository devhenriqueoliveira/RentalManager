using MediatR;
using RentalManager.Domain.Entities;

namespace RentalManager.Application.Motorcycles.Queries.GetByIdMotorcycle
{
    public record GetByIdMotorcycleQuery(Guid Id) : IRequest<Motorcycle>;
}
