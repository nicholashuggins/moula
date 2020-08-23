using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace Moula.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Ping()
        {
            var version = Assembly.GetEntryAssembly().GetName().Version;
            return Ok($"Moula Payments version {version}");
        }
    }
}