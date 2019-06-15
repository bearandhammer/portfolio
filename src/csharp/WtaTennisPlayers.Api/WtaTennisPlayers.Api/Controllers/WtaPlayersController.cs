using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using WtaTennisPlayers.Api.Cache;
using WtaTennisPlayers.Api.DataStore;
using WtaTennisPlayers.Api.Models;

namespace WtaTennisPlayers.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WtaPlayersController : Controller
    {
        #region Private Fields

        /// <summary>
        /// Represents an object to store our WTA Live results in.
        /// </summary>
        private readonly IMemoryCache cache;

        #endregion Private Fields

        #region Constants

        /// <summary>
        /// Represents the stock expiry for any element in the cache.
        /// </summary>
        private const int STOCK_CACHE_EXPIRY_MINS = 15;

        #endregion Constants

        #region Constructor

        /// <summary>
        /// Constructor for the WtaPlayersController which handles
        /// initialisation for caching (just this, at the moment).
        /// </summary>
        /// <param name="memCache">Represents a cache implementation.</param>
        public WtaPlayersController(IMemoryCache memCache)
        {
            cache = memCache;
        }

        #endregion Constructor

        #region API Endpoint

        /// <summary>
        /// The core API endpoint that illustrates the functionality of OData through the
        /// use of the EnableQuery() attribute.
        /// </summary>
        /// <returns>WTA player data (stock top 100 players) which is then further manipulated based on the callers query.</returns>
        [HttpGet]
        [EnableQuery()]
        public IEnumerable<WtaPlayer> GetPlayers()
        {
            // Attempt to retrieve WTA player data from the memory cache (or go and retrieve it, if necessary)
            if (!cache.TryGetValue(CacheKeys.PlayerData, out List<WtaPlayer> playerData))
            {
                // Initialise the WTA player data and store the results in the cache (with a stock expiry time)
                playerData = new PlayerDataStore().GetPlayerData();

                cache.Set(CacheKeys.PlayerData, playerData, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(STOCK_CACHE_EXPIRY_MINS)));
            }

            return playerData;
        }

        #endregion API Endpoint
    }
}