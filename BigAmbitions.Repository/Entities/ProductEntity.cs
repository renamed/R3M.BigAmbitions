namespace BigAmbitions.Repository.Entities;
public class ProductEntity : RegisterEntity
{
    public required string Name { get; set; }
    public int QtiPerBox { get; set; }

    public int BusinessId { get; set; }
    public BusinessEntity Business { get; set; }
}
