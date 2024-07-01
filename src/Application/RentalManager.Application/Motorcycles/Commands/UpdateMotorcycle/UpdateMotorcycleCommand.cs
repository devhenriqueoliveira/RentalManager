using MediatR;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;

namespace RentalManager.Application.Motorcycles.Commands.UpdateMotorcycle
{
    public record UpdateMotorcycleCommand(
        Guid Id,
        int Year,
        string Plate,
        string Model) : IRequest<BaseResponse<bool>>;
}
