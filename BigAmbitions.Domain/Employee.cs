namespace BigAmbitions.Domain;

public class Employee : IRegister
{
    public decimal Salary { get; set; }
    public int DailyHoursWork { get; set; }
    public int WeeklyDaysWork { get; set; }
    
    public Business Business { get; set; }
    public int BusinessId { get; set; }


    public int Id { get; set; }
}