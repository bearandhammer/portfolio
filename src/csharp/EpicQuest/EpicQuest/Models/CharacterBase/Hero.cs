using EpicQuest.Interfaces;
using EpicQuest.Models.CharacterBase;
using System;
using System.Collections.Generic;

namespace EpicQuest.Models
{
    /// <summary>
    /// Base class for all hero types.
    /// </summary>
    public abstract class Hero : CombatCharacter, ISupportsSetup, ICalculateDiceState, IHasEquipment
    {
        public Weapon HeroWeapon { get; set; }

        public Armour ChestSlot { get; set; }

        public Armour FeetSlot { get; set; }

        public Armour HandSlot { get; set; }

        public Armour HeadSlot { get; set; }

        public Armour LegSlot { get; set; }

        public Armour NecklaceSlot { get; set; }

        public Armour RingSlot { get; set; }

        public Hero(Weapon startingWeapon, Armour startingChestArmour, Armour startingFeetArmour, Armour startingHandArmour, Armour startingHeadArmour,
            Armour startingLegArmour, Armour startingNecklace, Armour startingRing)
        {
            HeroWeapon = startingWeapon == null ? new Weapon() : startingWeapon;
            ChestSlot = startingChestArmour == null ? new Armour(GameEnums.HeroDefensiveItemType.ChestRags) : startingChestArmour;

            //etc, etc
        }

        public virtual void CalculateDiceState()
        {
            AttackDice.Clear();
            AttackDice.AddRange(HeroWeapon.Dice);

            DefenceDice.Clear();
            DefenceDice.AddRange(ChestSlot.Dice);
            DefenceDice.AddRange(FeetSlot.Dice);
            DefenceDice.AddRange(HandSlot.Dice);
            DefenceDice.AddRange(HeadSlot.Dice);
            DefenceDice.AddRange(LegSlot.Dice);
            DefenceDice.AddRange(NecklaceSlot.Dice);
            DefenceDice.AddRange(RingSlot.Dice);
        }

        public abstract void PerformSetup();
    }
}