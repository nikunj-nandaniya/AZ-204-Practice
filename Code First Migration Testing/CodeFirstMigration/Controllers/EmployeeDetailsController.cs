using CodeFirstMigration.Models;
using CodeFirstMigration.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstMigration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeDetailsController : ControllerBase
    {
        private readonly IEmployeeDetailsRepository _employeeDetailsRepository;
        public EmployeeDetailsController(IEmployeeDetailsRepository employeeDetailsRepository) 
        {
            this._employeeDetailsRepository = employeeDetailsRepository;
        }

        [Route("/GetEmployeeDetails")]
        [HttpGet]
        public async Task<ActionResult> GetEmployeeDetails()
        {
            try
            {
                return Ok(await _employeeDetailsRepository.GetEmployeeDetails());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("/UpdateDepartment")]
        [HttpPost]
        public async Task UpdateDepartment(Department department)
        {
            try
            {
                await _employeeDetailsRepository.UpdateDepartment(department);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
