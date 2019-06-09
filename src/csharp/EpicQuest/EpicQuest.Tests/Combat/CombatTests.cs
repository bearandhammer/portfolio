using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicQuest.Interfaces;
using EpicQuest.Models;
using EpicQuest.Utility;

namespace EpicQuest.Tests.Combat
{
    /// <summary>
    /// Combat based entity specific Unity Tests
    /// for the Epic Quest game. 
    /// </summary>
    [TestClass]
    public class CombatTests
    {
        /// <summary>
        /// Test method that ensures that the StaticGameActions
        /// object (static) level HandleCombat method 
        /// is working as expected.  
        /// </summary>
        [TestMethod]
        public void CombatTestOne()
        {
            //Setup the aggressor and defender ICombatant supporting objects
            ICombatant aggressor = new Brawler(), defender = new Skeleton();

            //Handle the combat. Take the defenders previous health and obtain the damage done from StaticGameActions.HandleCombat call
            int defenderPreviousHealth = defender.Health, damageDone = StaticGameActions.HandleCombat(aggressor, defender);

            //Test that the defenders health has been subtracted properly (TODO - Handle dead defenders and make sure this works if a defender is overkilled)
            Assert.AreEqual<int>((defenderPreviousHealth - damageDone), defender.Health);
        }
    }
}