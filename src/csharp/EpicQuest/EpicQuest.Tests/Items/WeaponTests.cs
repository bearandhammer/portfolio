using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicQuest.Models;
using EpicQuest.Models.Dice;

namespace EpicQuest.Tests.Items
{
    /// <summary>
    /// Weapon entity specific Unit Tests
    /// for the Epic Quest game. 
    /// </summary>
    [TestClass]
    public class WeaponTests
    {
        /// <summary>
        /// Test method that ensures that the 
        /// Weapon object constructor is setting
        /// the HeroOffensiveItemType to 'Fists' by default.
        /// </summary>
        [TestMethod]
        public void WeaponTestDefaultIsFists()
        {
            //Create a new weapon, not specifying type
            Weapon fists = new Weapon();

            //Ensure that this is of type fists
            Assert.IsTrue(fists.OffensiveItemType == GameEnums.HeroOffensiveItemType.Fists,
                string.Format("WeaponTestDefaultIsFists (WeaponTests) failed. Incorrect default HeroOffensiveItemType applied, should be 'Fists' but found: {0}", fists.OffensiveItemType));
        }

        /// <summary>
        /// Test method that ensures that the Weapon
        /// object constructor is working as expected.
        /// </summary>
        [TestMethod]
        public void WeaponTestBasic()
        {
            //Create a new Weapon
            Weapon weaponItem = new Weapon(GameEnums.HeroOffensiveItemType.LegendaryBastardSword);

            //Ensure that the die associated with the object are as expected
            Assert.IsTrue((weaponItem.Dice.Where(die => die is BlueDie).Count() == 1 && weaponItem.Dice.Where(die => die is RedDie).Count() == 1
                && weaponItem.Dice.Count == 2), "WeaponTestBasic (WeaponTests) failed. Incorrect type of die/die count detected.");
        }
    }
}