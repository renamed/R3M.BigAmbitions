using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Entities;
public class BusinessEntity : RegisterEntity
{
    public required string Name { get; set; }

    public int DaysOpened { get; set; }

    public int ShelvesNeeded { get; set; }

    public required IList<Product> Products { get; set; }
}
