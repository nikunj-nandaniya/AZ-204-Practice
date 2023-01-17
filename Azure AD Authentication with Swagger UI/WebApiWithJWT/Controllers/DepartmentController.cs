using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIwithJWT.Repositories;

namespace WebAPIwithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [Route("/GetDepartments")]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetDepartments()
        {
            try
            {
                return Ok(await _departmentRepository.GetDepartments());
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
