using MediatR;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;

namespace RentalManager.Application.Motorcycles.Commands.DeleteMotorcycle
{
    public record DeleteMotorcycleCommand(Guid Id) : IRequest<BaseResponse<bool>>;
}
