using EpicQuest.GameEnums;

namespace EpicQuest.Models.Dice
{
    /// <summary>
    /// Public Sealed Class representing the strongest 
    /// dice type in Epic Dungeon. This will only be assigned
    /// to the strongest weapons/armour in the game.
    /// </summary>
    public sealed class RedDie : Die
    {
        #region Private Read Only Data Fields

        //Represents the Red Die 'faces' (aka the possible results from 'rolling' the die)
        private readonly DieFace[] dieFaces;

        #endregion Private Read Only Fields

        #region Overriden Properties

        /// <summary>
        /// Allow access to the die faces (results)
        /// that make up this dice.
        /// </summary>
        public override DieFace[] DieFaces
        {
            get 
            {
                return dieFaces;
            }
        }

        #endregion Overriden Properties

        #region Constructor

        /// <summary>
        /// RedDie Constructor that configures
        /// the results available on this Die.
        /// </summary>
        public RedDie()
        {
            dieFaces = new DieFace[6] { DieFace.Miss, DieFace.DoublePoint, DieFace.TriplePoint, DieFace.Miss, DieFace.TriplePoint, DieFace.CriticalPoint };
        }

        #endregion Constructor
    }
}