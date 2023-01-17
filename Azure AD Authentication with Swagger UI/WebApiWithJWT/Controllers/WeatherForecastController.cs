using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIwithJWT.Repositories;
using Microsoft.Identity.Web.Resource;

namespace WebAPIwithJWT.Controllers
{
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IDepartmentRepository _departmentRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDepartmentRepository departmentRepository)
        {
            _logger = logger;
            _departmentRepository = departmentRepository;
        }       
    }
}