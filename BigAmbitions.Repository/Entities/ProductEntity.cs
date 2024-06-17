namespace BigAmbitions.Repository.Entities;
public class ProductEntity : RegisterEntity
{
    public required string Name { get; set; }

    public int SoldLast7Days { get; set; }

    public int QtiToBuy { get; set; }

    public int BusinessBoxesNeeded { get; set; }
}
