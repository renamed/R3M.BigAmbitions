namespace BigAmbitions.Repository.Entities;
public class BusinessProductEntity : RegisterEntity
{
    public BusinessEntity Business { get; set; }
    public int BusinessId { get; set; }

    public ProductEntity Product { get; set; }
    public int ProductId { get; set; }
}
