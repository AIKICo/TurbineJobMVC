using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TurbineJobMVC.Models.ViewModels;
using TurbineJobMVC.Services;

namespace TurbineJobMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        public UsersController(
            ILogger<HomeController> logger,
            IMapper map,
            IService service,
            IDataProtectionProvider provider,
            IUserService userService)
            : base(logger, map, service, provider, userService)
        {
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateViewModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok();
        }
    }
}