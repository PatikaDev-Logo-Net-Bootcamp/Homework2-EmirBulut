using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootcampHomework2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet]
        [Route("/home")]
        public IActionResult Home()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/register")]
        public IActionResult Register()
        {
            return Ok("successfull request");
        }

        [HttpGet]
        [Route("/login")]
        public IActionResult Login()
        {
            return Ok("successfull request");
        }

        [HttpGet]
        [Route("/test")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Test()
        {
            return Ok();
        }


    }
}
