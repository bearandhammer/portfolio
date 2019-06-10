using EpicQuest.GameEnums;
using EpicQuest.Interfaces;
using EpicQuest.Models.Dice;
using System.Collections.Generic;

namespace EpicQuest.Models
{
    /// <summary>
    /// Public Class (not yet sealed, not sure of the plans here)
    /// representing a Weapon in Epic Quest.
    /// </summary>
    public class Weapon : ITreasure, IHeroWeapon
    {
        #region Private Data Fields

        //A list of 'dice' that get rolled (in attack) for any hero 'using' this Weapon
        private List<Die> dice = new List<Die>();

        //An Enum value that represent the Weapon Type being used
        private HeroOffensiveItemType offensiveItemType = HeroOffensiveItemType.Fists;

        #endregion Private Data Fields

        #region IRollsDice Interface Support (Properties)

        /// <summary>
        /// Provides access to the Attack Dice
        /// associated with this particular Weapon.
        /// </summary>
        public List<Die> Dice
        {
            get
            {
                return dice;
            }
            set
            {
                dice = value;
            }
        }

        #endregion IRollsDice Interface Support (Properties)

        #region ITreasure Interface Support (Properties)

        /// <summary>
        /// Return the GoldValue of this particular
        /// Weapon (currently fixed at 5). 
        /// </summary>
        public int GoldValue
        {
            get
            {
                return 5;
            }
        }

        #endregion ITreasure Interface Support (Properties)

        #region IHeroWeapon Interface Support (Properties)

        /// <summary>
        /// Provides access to the Weapong Type
        /// associated with this particular Weapon.
        /// </summary>
        public HeroOffensiveItemType OffensiveItemType
        {
            get 
            {
                return offensiveItemType; 
            }
	        set 
	        {
                offensiveItemType = value;

                //Reset the Dice associated with this Weapon (based on Type)
                Dice = GetDiceBasedOnOffensiveItemType(offensiveItemType);
	        }
        }

        #endregion IHeroWeapon Interface Support (Properties)

        #region Constructor

        /// <summary>
        /// Constructor for the Weapon Class
        /// that just sets the Weapon Type currently.
        /// </summary>
        /// <param name="itemType">The Weapon Type for this particular Weapon (default = Fists).</param>
        public Weapon(HeroOffensiveItemType itemType = HeroOffensiveItemType.Fists)
        {
            OffensiveItemType = itemType;
        }

        #endregion Constructor

        #region Public Static Methods

        /// <summary>
        /// Public static method that, based on the Weapon Type
        /// provided, returns the appropriate Attack Dice.
        /// </summary>
        /// <param name="itemType">The Weapon Type to inspect.</param>
        /// <returns>The dice that can be rolled based on the Weapon Type specified.</returns>
        public static List<Die> GetDiceBasedOnOffensiveItemType(HeroOffensiveItemType itemType)
        {
            List<Die> offensiveDice = new List<Die>();

            switch (itemType)
            {
                default:
                case HeroOffensiveItemType.Fists:
                case HeroOffensiveItemType.RustyDagger:
                case HeroOffensiveItemType.RustyMace:
                case HeroOffensiveItemType.RustyShortSword:
                    {
                        offensiveDice.Add(new GreenDie());
                    }
                    break;
                case HeroOffensiveItemType.RustyBastardSword:
                    {
                        offensiveDice.AddRange(new Die[] { new GreenDie(), new GreenDie() });
                    }
                    break;
                case HeroOffensiveItemType.SilverHexDagger:
                case HeroOffensiveItemType.MagicMissile:
                    {
                        offensiveDice.AddRange(new Die[] { new GreenDie(), new BlueDie() });
                    }
                    break;
                case HeroOffensiveItemType.FlameTorch:
                case HeroOffensiveItemType.Freeze:
                case HeroOffensiveItemType.FineBarbedWireMace:
                case HeroOffensiveItemType.JaggedSword:
                    {
                        offensiveDice.AddRange(new Die[] { new GreenDie(), new GreenDie(), new BlueDie() });
                    }
                    break;
                case HeroOffensiveItemType.Disolve:
                case HeroOffensiveItemType.LegendaryBastardSword:
                    {
                        offensiveDice.AddRange(new Die[] { new BlueDie(), new RedDie() });
                    }
                    break;
            }

            return offensiveDice;
        }

        #endregion Public Static Methods
    }
}