using CodeFirstMigration.Models;

namespace CodeFirstMigration.Repository
{
    public interface IEmployeeDetailsRepository
    {
        Task<IEnumerable<EmployeeDetails>> GetEmployeeDetails();

        Task UpdateDepartment(Department department);
    }
}
