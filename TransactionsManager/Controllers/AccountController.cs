using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;
using TransactionsManager.Services;

namespace TransactionsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(User user)
        {
            var registered = await _authService.Register(user);
            if (registered)
            {
                var token = _authService.GenerateToken(user.Login);
                return Ok("Bearer " + token);
            }
            return BadRequest("user already exists");
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(User user)
        {
            var registered = await _authService.GetUser(user);
            if (registered != null)
            {
                var token = _authService.GenerateToken(user.Login);
                return Ok("Bearer "+ token);
            }
            return BadRequest("there is no such user");
        }
    }
}