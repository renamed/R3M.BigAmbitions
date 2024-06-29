namespace BigAmbitions.Domain;

public class Business : IRegister, IName
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyRent { get; set; }
    public List<Employee> Employees { get; set; }

    public Game Game { get; set; }
    public int GameId { get; set; }
}