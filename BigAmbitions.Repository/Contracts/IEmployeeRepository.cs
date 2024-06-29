using BigAmbitions.Domain;

namespace BigAmbitions.Repository.Contracts
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task RemoveAsync(Employee employee);
        Task UpdateAsync(Employee employee);
    }
}
