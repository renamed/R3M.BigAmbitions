namespace BigAmbitions.Domain;

public class Business : IRegister
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal DailyRent { get; set; }

}