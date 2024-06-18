namespace BigAmbitions.Repository.Entities;
public class WarehouseBusinessEntity : RegisterEntity
{
    public WarehouseEntity Warehouse { get; set; }
    public int WarehouseId { get; set; }

    public BusinessEntity Business { get; set; }
    public int BusinessId { get; set; }
}
