using EpicQuest.GameEnums;
using EpicQuest.Interfaces;
using EpicQuest.Models.Dice;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EpicQuest.Models
{
    /// <summary>
    /// Public Class (not yet sealed, not sure of the plans here)
    /// representing a piece of Armour in Epic Quest.
    /// </summary>
    public class Armour : IHeroArmour, ITreasure
    {
        #region Constant Members

        //Constants that keep a track of the level 'boundaries' for item types (based on the HeroDefensiveItemType underlying byte value).
        //This system does indeed involve some manual maintenance of the enum types/these values but the engine code found in the 
        //GetDefensiveItemMappings method should take care of the rest
        private const int levelOneMax = 13, levelTwoMax = 23, levelThreeMax = 32, levelFourMax = 34, levelFiveMax = 36, levelMax = 5;

        #endregion Constant Members

        #region Private Data Fields

        //A list of 'dice' that get rolled (in defence) for any hero 'wearing' this Armour
        private List<Die> dice = new List<Die>();

        //Enum values that represent the Armour Type and what 'Body Slot' (i.e. Head, Chest) the Armour takes up
        private HeroDefensiveItemType defensiveItemType = HeroDefensiveItemType.None;
        private HeroDefensiveItemSlotType defensiveItemSlotType = HeroDefensiveItemSlotType.None;

        #endregion Private Data Fields

        #region ITreasure Interface Support (Properties)

        /// <summary>
        /// Return the GoldValue of this particular
        /// piece of Armour (currently fixed at 5).
        /// </summary>
        public int GoldValue
        {
            get
            {
                return 5;
            }
        }

        #endregion ITreasure Interface Support (Properties)

        #region IHeroArmour Interface Support (Properties)

        /// <summary>
        /// Provides access to the Armour Type
        /// associated with this particular piece of Armour.
        /// </summary>
        public HeroDefensiveItemType DefensiveItemType
        {
            get
            {
                return defensiveItemType;
            }
            set
            {
                defensiveItemType = value;

                //Reset the Dice associated with this piece of Armour (based on Type)
                Dice = GetDiceBasedOnDefensiveItemType(value);
            }
        }

        /// <summary>
        /// Provides access to the Armour Type Slot
        /// associated with this particular piece of Armour.
        /// </summary>
        public HeroDefensiveItemSlotType DefensiveItemTypeSlot
        {
            get
            {
                return defensiveItemSlotType;
            }
            private set
            {
                defensiveItemSlotType = value;
            }
        }

        #endregion IHeroArmour Interface Support (Properties)

        #region IRollsDice Interface Support (Properties)

        /// <summary>
        /// Provides access to the Defence Dice
        /// associated with this particular piece of Armour.
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

        #region Static Data Fields (Armour Mappings)

        //Private static backing dictionary (for all classes to use) that provides a mapping 'table' on Armour Types to Armour Slots (and the level of these Armour Types)
        private static Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int> armourMappings;

        #endregion Static Data Fields (Armour Mappings)

        #region Static Properties (Armour Mappings)

        /// <summary>
        /// Allows access to the static Armour Mapping
        /// dictionary so other classes can view Armour Type,
        /// Armour Slot to Level mappings (so Armour can be placed into
        /// the right slots on a Hero/to check the hero is of the
        /// appropriate level to wear a piece of Armour.
        /// </summary>
        public static Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int> ArmourMappings
        {
            get
            {
                return armourMappings;
            }
            private set
            {
                armourMappings = value;
            }
        }

        #endregion Static Properties (Armour Mappings)

        #region Static Constructor

        /// <summary>
        /// Static constructor that fires on first use of this class
        /// to setup Armour Types/Slots and Level mappings.
        /// </summary>
        static Armour()
        {
            armourMappings = GetDefensiveItemMappings();
        }

        #endregion Static Constructor

        #region Constructor

        public Armour(HeroDefensiveItemType itemType)
        {
            DefensiveItemType = itemType;
            DefensiveItemTypeSlot = GetSlotTypeBasedOnArmourType(itemType);
        }

        #endregion Constructor

        #region public Static Methods

        /// <summary>
        /// Public static method that returns a 'Slot Type'
        /// based on an Armour Type.
        /// </summary>
        /// <param name="itemType">The Armour Type to inspect.</param>
        /// <returns>The Slot Type this piece of Armour belongs to.</returns>
        public static HeroDefensiveItemSlotType GetSlotTypeBasedOnArmourType(HeroDefensiveItemType itemType)
        {
            switch (itemType)
            {
                case HeroDefensiveItemType.ClothTunic:
                case HeroDefensiveItemType.LeatherChestPiece:
                case HeroDefensiveItemType.NoviceMagicianRobe:
                case HeroDefensiveItemType.NoviceRobeOfSkulls:
                case HeroDefensiveItemType.AdeptMagicianDragonSkinRobe:
                case HeroDefensiveItemType.AdeptRobeOfDemonSkulls:
                case HeroDefensiveItemType.ChainMailChestPiece:
                case HeroDefensiveItemType.DragonSkinChestPiece:
                case HeroDefensiveItemType.MasterworkPlateChestPiece:
                case HeroDefensiveItemType.BloodStainedMithralArmour:
                case HeroDefensiveItemType.BloodStainedMithralRobe:
                    {
                        return HeroDefensiveItemSlotType.Chest;
                    }
                case HeroDefensiveItemType.LeatherHelm:
                case HeroDefensiveItemType.ChainMailHelm:
                case HeroDefensiveItemType.MasterworkPlateHelm:
                    {
                        return HeroDefensiveItemSlotType.Head;
                    }
                case HeroDefensiveItemType.LeatherGloves:
                case HeroDefensiveItemType.ChainMailGloves:
                case HeroDefensiveItemType.MasterworkPlateGloves:
                    {
                        return HeroDefensiveItemSlotType.Hands;
                    }
                case HeroDefensiveItemType.LeatherBoots:
                case HeroDefensiveItemType.ChainMailBoots:
                case HeroDefensiveItemType.MasterworkPlateBoots:
                    {
                        return HeroDefensiveItemSlotType.Feet;
                    }
                case HeroDefensiveItemType.LeatherPants:
                case HeroDefensiveItemType.ChainMailPants:
                case HeroDefensiveItemType.MasterworkPlatePants:
                    {
                        return HeroDefensiveItemSlotType.Legs;
                    }
                case HeroDefensiveItemType.SilverNecklace:
                case HeroDefensiveItemType.GoldNecklace:
                    {
                        return HeroDefensiveItemSlotType.Necklace;
                    }
                case HeroDefensiveItemType.SilverRing:
                case HeroDefensiveItemType.GoldRing:
                    {
                        return HeroDefensiveItemSlotType.Ring;
                    }
                case HeroDefensiveItemType.None:
                default:
                    {
                        return HeroDefensiveItemSlotType.None;
                    }
            }
        }

        /// <summary>
        /// Public static method that returns the 'Dice' that
        /// can be used based on the type of Armour specified.
        /// </summary>
        /// <param name="itemType">The Armour Type to inspect.</param>
        /// <returns>A List of Dice (Die) objects that can be rolled based on the Armour Type specified.</returns>
        public static List<Die> GetDiceBasedOnDefensiveItemType(HeroDefensiveItemType itemType)
        {
            List<Die> defensiveDice = new List<Die>();

            switch (itemType)
            {
                default:
                case HeroDefensiveItemType.ClothTunic:
                case HeroDefensiveItemType.LeatherHelm:
                case HeroDefensiveItemType.LeatherGloves:
                case HeroDefensiveItemType.LeatherBoots:
                case HeroDefensiveItemType.LeatherPants:
                case HeroDefensiveItemType.SilverNecklace:
                case HeroDefensiveItemType.SilverRing:
                    {
                        defensiveDice.Add(new GreenDie());
                    }
                    break;
                case HeroDefensiveItemType.LeatherChestPiece:
                case HeroDefensiveItemType.GoldNecklace:
                case HeroDefensiveItemType.GoldRing:
                    {
                        defensiveDice.AddRange(new Die[] { new GreenDie(), new GreenDie() });
                    }
                    break;
            }

            return defensiveDice;
        }

        #endregion Public Static Methods

        #region Private Static Methods

        /// <summary>
        /// Private static method that configures (and returns a dictionary representing)
        /// Armour Type to Armour Slot mappings (with a value denoting what level a hero
        /// needs to be to wear a piece of Armour).
        /// </summary>
        /// <returns>A dictionary that represents Armour Type to Armour Slot mappings (with level information).</returns>
        private static Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int> GetDefensiveItemMappings()
        {
            Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int> mappings = new Dictionary<KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>, int>();

            //Temporary try/catch (put in during development as this piece of code evolves)
            try
            {
                //There are currently '5' (or whatever levelMax is set to) levels of Armour Items present in the game - Loop 5 times to capture all Armour Types
                for (int level = 1; level <= levelMax; level++)
                {
                    //Get values in the HeroDefensiveItemType enum and get the ones belong to the current level (based on for loop)
                    Enum.GetValues(typeof(HeroDefensiveItemType)).Cast<HeroDefensiveItemType>().Where(item => GetLevelConditionFilter(level, item)).ToList<HeroDefensiveItemType>().ForEach(item =>
                    {
                        //Store a reference, in the mappings dictionary, to the Armour Type, the Slot Type the Armour belongs to (via the GetSlotTypeBasedOnArmourType method) and the level this
                        //item belongs to (represented by the level variable)
                        HeroDefensiveItemSlotType slotType = GetSlotTypeBasedOnArmourType(item);
                        mappings.Add(new KeyValuePair<HeroDefensiveItemType, HeroDefensiveItemSlotType>(item, slotType), level);   
                    });
                }
            }
            catch (Exception ex)
            {
                //Write errors to the console (to be removed) - No need to interrupt the flow of the game for now (broken mappings = broken game, so the player would need to close anyway)
                Console.WriteLine(ex.Message);
            }

            return mappings;
        }

        /// <summary>
        /// Private static method that returns a boolean determining in the 
        /// Armour Type specified belongs to the current level value passed in.
        /// </summary>
        /// <param name="level">The level to check against (does the Armour specified belong to this level).</param>
        /// <param name="item">The Armour Type to check.</param>
        /// <returns>If the Armour Type belongs to the specified level then true, otherwise false. An Armour Type of HeroDefensiveItemType.None will always result in false.</returns>
        private static bool GetLevelConditionFilter(int level, HeroDefensiveItemType item)
        {
            //When using this filter an Armour Type of None should never be included, return false
            if (item == HeroDefensiveItemType.None)
            {
                return false;
            }

            //Only return true if an Armour Types underlying byte type falls between the level min/max limits specified (by constants in this class)
            switch (level)
            {
                default:
                case 1:
                    {
                        return ((byte)item > 0 && (byte)item <= levelOneMax);
                    }
                case 2:
                    {
                        return ((byte)item > levelOneMax && (byte)item <= levelTwoMax);
                    }
                case 3:
                    {
                        return ((byte)item > levelTwoMax && (byte)item <= levelThreeMax);
                    }
                case 4:
                    {
                        return ((byte)item > levelThreeMax && (byte)item <= levelFourMax);
                    }
                case 5:
                    {
                        return ((byte)item > levelFourMax && (byte)item <= levelFiveMax);
                    }
            }
        }

        #endregion Private Static Methods
    }
}