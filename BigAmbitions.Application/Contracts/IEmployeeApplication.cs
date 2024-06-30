using BigAmbitions.Domain;

namespace BigAmbitions.Application.Contracts;
public interface IEmployeeApplication
{
    Task<Employee> AddAsync(Employee employee);
    Task<Employee> EditAsync(int id, Employee employee);
    ValueTask<Employee?> FindAsync(int id);
    Task RemoveAsync(int id);
}
