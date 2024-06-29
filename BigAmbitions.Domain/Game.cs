namespace BigAmbitions.Domain;

public class Game : IRegister, IName
{
    public int Id { get; set; }

    public string Name { get; set; }

    public List<Business> Businesses { get; set; }
}
