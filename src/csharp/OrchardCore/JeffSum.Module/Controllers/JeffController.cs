using JeffSum.Module.ViewModels;
using JeffSum.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JeffSum.Module.Controllers
{
    /// <summary>
    /// A controller that exposes Jeff Goldblum
    /// style Ipsum content.
    /// </summary>
    [Route("GiveMeJeff")]
    public class JeffController : Controller
    {
        /// <summary>
        /// Returns an <see cref="IActionResult"/> for rendering
        /// Jeff Goldblum style Ipsum content to the appropriate View.
        /// </summary>
        /// <param name="jeffIpsumService">A service for 'receiving the Jeff'.</param>
        /// <returns>Jeff Goldblum Ipsum content to the designated View (fixed to 5 paragraphs, for example purposes).</returns>
        public async Task<IActionResult> Index([FromServices] IJeffIpsumService jeffIpsumService) =>
            View(new JeffIpsumViewModel(string.Join($"{ Environment.NewLine }{ Environment.NewLine }", await jeffIpsumService.GetIpsumParagraphs(5))));
    }
}
