using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIwithJWT.Data;
using WebAPIwithJWT.Models;

namespace WebAPIwithJWT.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private AppDbContext appDbContext;
        public DepartmentRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Department> GetDepartment(int departmentId)
        {
            return await appDbContext.departments
                        .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
        }

        public async Task<IEnumerable<Department>> GetDepartments() => await appDbContext.departments.ToListAsync();
    }
}

