using FluentValidation;
using MediatR;
using RentalManager.Application.Motorcycles.Commands.CreateMotorcycle;
using RentalManager.Application.Motorcycles.Commands.DeleteMotorcycle;
using RentalManager.Application.Motorcycles.Commands.UpdateMotorcycle;
using RentalManager.Application.Motorcycles.Queries.GetAllMotorcycle;
using RentalManager.Infrastructure.CrossCutting.Commons.Caching;
using RentalManager.Infrastructure.Data.Contexts;
using RentalManager.Infrastructure.Data.Interfaces;
using RentalManager.Infrastructure.Data.Repositories;
using RentalManager.Infrastructure.Data.UnitOfWork;
using RentalManager.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDapperDatabaseContext>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreConnection");
    return new DapperDatabaseContext(connectionString!);
});

builder.Services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(CreateMotorcycleCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(DeleteMotorcycleCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(UpdateMotorcycleCommand).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(GetAllMotorcycleQuery).Assembly));
//builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
builder.Services.AddMemoryCache();
builder.Services.AddValidatorsFromAssemblyContaining<CreateMotorcycleValidator>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapMotorcycleEndpoint();
app.Run();