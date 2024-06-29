using BigAmbitions.Domain;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BigAmbitions.Repository
{
    public class EmployeeRepository(DbContext dbContext) 
        : AbstractRepository<Employee>(dbContext), IEmployeeRepository
    {
    }
}
