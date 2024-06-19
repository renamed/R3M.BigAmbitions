using BigAmbitions.Application.Contracts;
using BigAmbitions.Application.Extensions;
using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using FluentValidation;
using System.Diagnostics.CodeAnalysis;

namespace BigAmbitions.Application;

public class WarehouseApplication : IWarehouseApplication
{
    private readonly IWarehouseRepository warehouseRepository;
    private readonly IValidator<Warehouse> validator;

    public WarehouseApplication(IWarehouseRepository warehouseRepository, IValidator<Warehouse> validator)
    {
        this.warehouseRepository = warehouseRepository;
        this.validator = validator;
    }

    public async ValueTask<Response<Warehouse>> AddAsync([NotNull] Warehouse warehouse)
    {
        var response = warehouse.ValidateFull(validator);
        await warehouseRepository.AddAsync(warehouse);
        return response;
    }

    public async ValueTask<Response<Warehouse>> DeleteAsync([NotNull] Warehouse warehouse)
    {
        var response = warehouse.Validate(validator);
        await warehouseRepository.DeleteAsync(warehouse.Id);
        return response;
    }
}
