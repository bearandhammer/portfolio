using DinoIpsum.Module.ViewModels;
using DinoIpsum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DinoIpsum.Module.Controllers
{
    [Route("GiveMeDino")]
    public class DinoController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IDinoIpsumService dinoIpsumService) =>
            View(new DinoIpsumViewModel(await dinoIpsumService.GetIpsumParagraphs(3)));
    }
}
