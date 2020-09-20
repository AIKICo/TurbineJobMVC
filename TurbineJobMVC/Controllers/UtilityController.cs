using System;
using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TurbineJobMVC.Services;

namespace TurbineJobMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : BaseApiController
    {
        public UtilityController(
            ILogger<HomeController> logger,
            IMapper map,
            IService service,
            IDataProtectionProvider provider,
            IUserService userService)
            : base(logger, map, service, provider, userService)
        {
        }

        [HttpGet("GetServerDate")]
        public ActionResult<DateTime> GetServerDate()
        {
            return Ok(DateTime.Now);
        }
    }
}