using Devices.API.Contracts;
using Devices.API.Request;
using Devices.API.Response;
using Devices.Application.UseCases.DeviceUseCase;
using Devices.Application.UseCases.DeviceUseCase.CreateDevice;
using Devices.Application.UseCases.DeviceUseCase.DeleteDevice;
using Devices.Application.UseCases.DeviceUseCase.GetDeviceById;
using Devices.Application.UseCases.DeviceUseCase.ListDevices;
using Devices.Application.UseCases.DeviceUseCase.PatchDevice;
using Devices.Application.UseCases.DeviceUseCase.UpdateDevice;
using Devices.Domain;
using Devices.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devices.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceUseCases _service;

        public DevicesController(IDeviceUseCases service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<DeviceResponse>> Create([FromBody] CreateDeviceRequest request, CancellationToken ct)
        {
            var result = await _service.CreateAsync(new CreateDeviceCommand(request.Name, request.Brand, request.State switch
            {
                DeviceStateDto.Available => "Available",
                DeviceStateDto.InUse => "InUse",
                DeviceStateDto.Inactive => "Inactive",
                _ => throw new DomainException("Invalid device state.")
            }), ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DeviceResponse>> GetById(Guid id, CancellationToken ct)
        {
            var device = await _service.GetByIdAsync(new GetDeviceByIdQuery(id), ct);
            return Ok(device);
        }

        [HttpGet]
        public async Task<ActionResult<List<DeviceResponse>>> GetAll([FromQuery] string? brand, [FromQuery] string? state, CancellationToken ct)
        {
            var list = await _service.ListAsync(new ListDevicesQuery(brand, state), ct);
            return Ok(list);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DeviceResponse>> Update(Guid id, [FromBody] UpdateDeviceRequest request, CancellationToken ct)
        {
            var updated = await _service.UpdateAsync(new UpdateDeviceCommand(id, request.Name, request.Brand, request.State switch
            {
                DeviceStateDto.Available => "Available",
                DeviceStateDto.InUse => "InUse",
                DeviceStateDto.Inactive => "Inactive",
                _ => throw new DomainException("Invalid device state.")
            }), ct);
            return Ok(updated);
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<DeviceResponse>> Patch(Guid id, [FromBody] PatchDeviceRequest request, CancellationToken ct)
        {
            var updated = await _service.PatchAsync(new PatchDeviceCommand(id, request.Name, request.Brand, request.State switch
            {
                DeviceStateDto.Available => "Available",
                DeviceStateDto.InUse => "InUse",
                DeviceStateDto.Inactive => "Inactive",
                _ => throw new DomainException("Invalid device state.")
            }), ct);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.DeleteAsync(new DeleteDeviceCommand(id), ct);
            return NoContent();
        }
    }
}
