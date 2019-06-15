using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WtaTennisPlayers.Api.Models;

namespace WtaTennisPlayers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WtaPlayersController : Controller
    {
        [HttpGet]
        [EnableQuery()]
        public IEnumerable<WtaPlayer> GetPlayers()
        {
            return new List<WtaPlayer>
            {
                new WtaPlayer(1, "Naomi Osaka", 12000),
                new WtaPlayer(2, "Test Test", 9000),
                new WtaPlayer(3, "Amy Test", 8950),
                new WtaPlayer(4, "Another Test", 5000),
                new WtaPlayer(5, "Last Player", 1250)
            };
        }
    }
}