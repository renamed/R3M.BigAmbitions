namespace BigAmbitions.Domain;

public class Employee : IRegister
{
    public decimal Salary { get; set; }
    public int DailyHoursWork { get; set; }
    public int WeeklyDaysWork { get; set; }

    public decimal SalaryByWeek => DailyHoursWork * WeeklyDaysWork;

    public int Id { get; set; }
}