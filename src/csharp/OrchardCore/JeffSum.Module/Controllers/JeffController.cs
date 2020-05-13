using Jeff.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
