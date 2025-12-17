using Devices.Application.Common;
using Devices.Application.UseCases.DeviceUseCase;
using Devices.Application.UseCases.DeviceUseCase.CreateDevice;
using Devices.Application.UseCases.DeviceUseCase.DeleteDevice;
using Devices.Application.UseCases.DeviceUseCase.GetDeviceById;
using Devices.Application.UseCases.DeviceUseCase.ListDevices;
using Devices.Application.UseCases.DeviceUseCase.PatchDevice;
using Devices.Application.UseCases.DeviceUseCase.UpdateDevice;

namespace Devices.Application;


public sealed class DeviceUseCases : IDeviceUseCases
{
    private readonly CreateDeviceUseCase _create;
    private readonly GetDeviceByIdUseCase _getById;
    private readonly ListDevicesUseCase _list;
    private readonly UpdateDeviceUseCase _update;
    private readonly PatchDeviceUseCase _patch;
    private readonly DeleteDeviceUseCase _delete;

    public DeviceUseCases(
        CreateDeviceUseCase create,
        GetDeviceByIdUseCase getById,
        ListDevicesUseCase list,
        UpdateDeviceUseCase update,
        PatchDeviceUseCase patch,
        DeleteDeviceUseCase delete)
    {
        _create = create;
        _getById = getById;
        _list = list;
        _update = update;
        _patch = patch;
        _delete = delete;
    }

    public Task<DeviceDto> CreateAsync(CreateDeviceCommand cmd, CancellationToken ct)
        => _create.HandleAsync(cmd, ct);

    public Task<DeviceDto?> GetByIdAsync(GetDeviceByIdQuery query, CancellationToken ct)
        => _getById.HandleAsync(query, ct);

    public Task<IReadOnlyList<DeviceDto>> ListAsync(ListDevicesQuery query, CancellationToken ct)
        => _list.HandleAsync(query, ct);

    public Task<DeviceDto> UpdateAsync(UpdateDeviceCommand cmd, CancellationToken ct)
        => _update.HandleAsync(cmd, ct);

    public Task<DeviceDto> PatchAsync(PatchDeviceCommand cmd, CancellationToken ct)
        => _patch.HandleAsync(cmd, ct);

    public Task DeleteAsync(DeleteDeviceCommand cmd, CancellationToken ct)
        => _delete.HandleAsync(cmd, ct);
}
