namespace BigAmbitions.Repository.Entities;

public class ProductConfigEntity : RegisterEntity
{
    public required string Name { get; set; }
        
    public int QtiPerBox { get; set; }
}
