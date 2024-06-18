namespace BigAmbitions.Domain;

public class Warehouse
{
    public int Id { get; set; }
    public string Name { get; set; }

    public List<Business> Businesses { get; set; } = [];

    public bool HasBusinesses => Businesses.Count > 0;

    //public int ShelvesNeeded => (int)Math.Ceiling(Businesses.Sum(x => x.Products.Sum(y => y.BusinessBoxesNeeded)) / 60.0);

    //public IReadOnlyDictionary<string, int> QtiToBuyProducts => CalcQtiToBuyProducts();

    //private IReadOnlyDictionary<string, int> CalcQtiToBuyProducts()
    //{
    //   return Businesses.SelectMany(sm => sm.Products)
    //        .GroupBy(gb => gb.Name)
    //        .Select(s => new
    //        {
    //            s.First().Name,
    //            Qti = s.Sum(sum => sum.QtiToBuy)
    //        })
    //        .ToDictionary(k => k.Name, v => v.Qti);
    //}
}
