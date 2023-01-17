using LoggerService;
using Microsoft.AspNetCore.Mvc;
using WebAPIwithJWT.Models;
using WebAPIwithJWT.Repositories;

namespace WebAPIwithJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private ILoggerManager _logger;

        public AuthController(IAuthRepository authRepo, ILoggerManager logger)
        {
            _authRepo = authRepo;
            _logger = logger;
        }

        [HttpPost("/register")]
        public async Task<IActionResult> Register(UserRegister request)
        {
            var _user = new User
            {
                Email = request.Email,
            };

            var response = await _authRepo.Register(_user,request.Password);            

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("/login")]
        public async Task<IActionResult>Login(UserLogin request)
        {
            //_logger.LogInfo("Fetching Login Details.");

            //throw new Exception("Nik exception thrown");
            var response = await _authRepo.Login(request.Email, request.Password);

            _logger.LogInfo("login details received.");

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
