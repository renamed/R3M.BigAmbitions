using System.Text;

namespace BigAmbitions.Domain;
public class Business
{
    public string Name { get; set; }

    public int DaysOpened { get; set; }

    public int ShelvesNeeded => (int)Math.Ceiling(Products.Sum(s => s.BusinessBoxesNeeded) / 16.0);

    public List<Product> Products { get; set; } = [];
}
