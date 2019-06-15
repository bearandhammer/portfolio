using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using WtaTennisPlayers.Api.Models;

namespace WtaTennisPlayers.Api.DataStore
{
    /// <summary>
    /// Represents a simple mock data store for retrieving the top
    /// 100 WTA tennis players (using https://live-tennis.eu/en/wta-live-ranking).
    /// </summary>
    public class PlayerDataStore
    {
        #region Constants

        /// <summary>
        /// This is the current live ranking WTA website address.
        /// </summary>
        private const string WTA_LIVE_RANKING_SITE = "https://live-tennis.eu/en/wta-live-ranking";

        #endregion Constants

        #region Data Store Public Methods

        /// <summary>
        /// Uses the types found within the HtmlAgilityPack NuGet package to retrieve the
        /// top 100 WTA tennis players from the live-tennis.eu website (a small touch of HTML scraping in place).
        /// </summary>
        /// <returns>A List of <see cref="WtaPlayer"/> types, to be further interrogated based on the users query.</returns>
        public List<WtaPlayer> GetPlayerData()
        {
            List<WtaPlayer> liveRankingPlayers = new List<WtaPlayer>();

            try
            {
                // Retrieve a HtmlDocument type that contains the live ranking table data and create WTA player objects from this
                HtmlDocument wtaPlayerHtmlDocument = GetLiveRankingsWebDocument();

                if (wtaPlayerHtmlDocument != null)
                {
                    liveRankingPlayers.AddRange(GetPlayersFromHtmlDocument(wtaPlayerHtmlDocument));
                }
            }
            catch (Exception ex)
            {
                // Add appropriately logging, on error, as required...
                Debug.WriteLine(ex.Message);
            }

            return liveRankingPlayers;
        }

        #endregion Data Store Public Methods

        #region Data Store Private Static Methods

        /// <summary>
        /// Simple static helper method that gets the HtmlDocument with WTA live ranking HTML.
        /// </summary>
        /// <returns></returns>
        private static HtmlDocument GetLiveRankingsWebDocument() => new HtmlWeb().Load(WTA_LIVE_RANKING_SITE);

        /// <summary>
        /// Static helper method that tears apart the provided HtmlDocument to construct
        /// a collection of WtaPlayer objects.
        /// </summary>
        /// <param name="wtaPlayerHtmlDocument">The HtmlDocument that contains the WTA player data.</param>
        /// <returns>An IEnumerable of type <see cref="WtaPlayer"/>.</returns>
        private static IEnumerable<WtaPlayer> GetPlayersFromHtmlDocument(HtmlDocument wtaPlayerHtmlDocument)
        {
            // Work with the returned HTML data - first step is to identify the table rows (only a single table on the page at
            // the time of producing this code sample). We only want every other 'tr', as these are the only ones that contain data, also
            // keeping in mind that spacer 'tr' elements exist (without a 'td' Count of 14, so also remove these elements)
            HtmlNodeCollection tbodyRowNodes = wtaPlayerHtmlDocument.DocumentNode.SelectNodes("//tbody/tr");

            IEnumerable<HtmlNode> everyOtherNode = tbodyRowNodes
                .Where((node, index) => index % 2 == 0 && node?.ChildNodes?.Count == 14)
                .Take(100);

            // Setup a regex to clean up the rank information
            Regex rankCleanerRegex = new Regex(@"<[^>]+>|&nbsp;");

            // Construct and return WtaPlayer objects based on some very (fixed, agreed!) ripping of text from td elements
            return everyOtherNode.Select(node =>
                 new WtaPlayer(int.Parse(rankCleanerRegex.Replace(node.ChildNodes[0].InnerText, string.Empty).Trim()),
                    node.ChildNodes[3].InnerText.Trim(),
                    int.Parse(node.ChildNodes[6].InnerText.Trim())));
        }

        #endregion Data Store Private Static Methods
    }
}