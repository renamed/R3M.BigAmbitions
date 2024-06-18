namespace BigAmbitions.Repository.Entities;
public class WarehouseEntity : RegisterEntity
{
    public string Name { get; set; }
    public List<BusinessEntity> Businesses { get; set; }
}
