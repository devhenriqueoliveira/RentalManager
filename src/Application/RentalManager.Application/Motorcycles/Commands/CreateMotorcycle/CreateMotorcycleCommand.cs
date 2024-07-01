using MediatR;
using RentalManager.Infrastructure.CrossCutting.Commons.Bases;

namespace RentalManager.Application.Motorcycles.Commands.CreateMotorcycle
{
    public record CreateMotorcycleCommand(
        int Year, 
        string Plate, 
        string Model) : IRequest<BaseResponse<bool>>;
}
