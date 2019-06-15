namespace WtaTennisPlayers.Api.Models
{
    public class WtaPlayer
    {
        public int Rank { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public WtaPlayer(int playerRank, string playerName, int playerPoints)
        {
            Rank = playerRank;
            Name = playerName;
            Points = playerPoints;
        }
    }
}