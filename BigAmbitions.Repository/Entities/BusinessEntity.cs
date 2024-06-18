using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Entities;
public class BusinessEntity : RegisterEntity
{
    public required string Name { get; set; }


    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
}
