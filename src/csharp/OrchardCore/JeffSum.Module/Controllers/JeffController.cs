using Jeff.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JeffSum.Module.Controllers
{
    [Route("GiveMeJeff")]
    public class JeffController : Controller
    {
        public IActionResult Index([FromServices] IJeffIpsumService jeffIpsumService)
        {
            return View();
        }
    }
}
