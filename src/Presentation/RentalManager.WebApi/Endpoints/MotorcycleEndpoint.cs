using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentalManager.Application.Motorcycles.Commands.CreateMotorcycle;
using RentalManager.Application.Motorcycles.Commands.DeleteMotorcycle;
using RentalManager.Application.Motorcycles.Commands.UpdateMotorcycle;
using RentalManager.Application.Motorcycles.Queries.GetAllMotorcycle;
using RentalManager.Application.Motorcycles.Queries.GetByIdMotorcycle;

namespace RentalManager.WebApi.Endpoints
{
    public static class MotorcycleEndpoint
    {
        public static void MapMotorcycleEndpoint(this WebApplication app)
        {
            #region POST

            app.MapPost("/api/motorcycles", async (
                CreateMotorcycleCommand command, 
                ISender mediator) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithName("CreateMotorcycle")
            .WithOpenApi();

            #endregion

            app.MapPut("/api/motorcycles/{id}", async (
                ISender mediator,
                [FromBody] UpdateMotorcycleCommand command,
                [FromRoute] Guid id) =>
            {
                var result = await mediator.Send(command);
                return Results.Ok(result);
            })
            .WithName("UpdateMotorcycle")
            .WithOpenApi();

            app.MapDelete("/api/motorcycles/{id}", async (
                ISender mediator,
                Guid id) =>
            {
                var result = await mediator.Send(new DeleteMotorcycleCommand(id));
                return Results.Ok(result);
            })
            .WithName("DeleteMotorcycle")
            .WithOpenApi();

            app.MapGet("api/motorcycles", 
                async (ISender mediator) =>
            {
                var motorcycles = await mediator.Send(new GetAllMotorcycleQuery());
                return Results.Ok(motorcycles);
            })
            .WithName("GetAllMotorcycle")
            .WithOpenApi();

            app.MapGet("api/motorcycles/{id}", 
                async (Guid id, ISender mediator) =>
            {
                var motorcycles = await mediator.Send(new GetByIdMotorcycleQuery(id));
                return Results.Ok(motorcycles);
            })
            .WithName("GetByIdMotorcycle")
            .WithOpenApi();
        }
    }
}
