using BigAmbitions.Domain;
using BigAmbitions.Repository.Contexts;
using BigAmbitions.Repository.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BigAmbitions.Repository;

public class EmployeeRepository(BigAmbitionContext dbContext) 
    : AbstractRepository<Employee>(dbContext), IEmployeeRepository
{
}
