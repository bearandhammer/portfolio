using HipsterIpsum.Module.ViewModels;
using HipsterIpsum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HipsterIpsum.Module.Controllers
{
    [Route("GiveMeHipster")]
    public class HipsterController : Controller
    {
        public async Task<IActionResult> Index([FromServices] IHipsterIpsumService hipsterIpsumService) => 
            View(new HipsterIpsumViewModel(await hipsterIpsumService.GetIpsumParagraphs(3)));
    }
}
