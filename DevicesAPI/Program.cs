using Devices.Application;
using Devices.Application.Abstractions;
using Devices.Application.UseCases.DeviceUseCase;
using Devices.Application.UseCases.DeviceUseCase.CreateDevice;
using Devices.Application.UseCases.DeviceUseCase.DeleteDevice;
using Devices.Application.UseCases.DeviceUseCase.GetDeviceById;
using Devices.Application.UseCases.DeviceUseCase.ListDevices;
using Devices.Application.UseCases.DeviceUseCase.PatchDevice;
using Devices.Application.UseCases.DeviceUseCase.UpdateDevice;
using Devices.Infrastructure;
using Devices.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<CreateDeviceUseCase>();
builder.Services.AddScoped<GetDeviceByIdUseCase>();
builder.Services.AddScoped<ListDevicesUseCase>();
builder.Services.AddScoped<UpdateDeviceUseCase>();
builder.Services.AddScoped<PatchDeviceUseCase>();
builder.Services.AddScoped<DeleteDeviceUseCase>();

builder.Services.AddScoped<IDeviceUseCases, DeviceUseCases>();
builder.Services
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<DevicesDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DevicesDb")));

builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Devices API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();