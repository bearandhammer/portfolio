using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicQuest.Models;
using EpicQuest.Models.Dice;

namespace EpicQuest.Tests.Items
{
    /// <summary>
    /// Armour entity specific Unit Tests
    /// for the Epic Quest game. 
    /// </summary>
    [TestClass]
    public class ArmourTests
    {
        /// <summary>
        /// Test method that ensures that the Armour
        /// object constructor is working as expected. 
        /// </summary>
        [TestMethod]
        public void ArmourTestBasic()
        {
            //Die property is publically accessible??? TO CHECK; Either way create a new, known armour type
            Armour armourPiece = new Armour(GameEnums.HeroDefensiveItemType.LeatherChestPiece);

            //Ensure the die for this piece of armour are correct
            Assert.IsTrue((armourPiece.Dice.Where(die => die is GreenDie).Count() == 2 && armourPiece.Dice.Count == 2), "ArmourTestBasic (ArmourTests) failed. Incorrect type of die/die count detected.");
        }

        [TestMethod]
        public void ArmourStaticGetSlotBasedOnArmourTypeTestOne()
        {
            Assert.IsTrue(Armour.GetSlotTypeBasedOnArmourType(GameEnums.HeroDefensiveItemType.None) == GameEnums.HeroDefensiveItemSlotType.None,
                "ArmourStaticGetSlotBasedOnArmourTypeTestOne (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        [TestMethod]
        public void ArmourStaticGetSlotBasedOnArmourTypeTestTwo()
        {
            Assert.IsTrue(Armour.GetSlotTypeBasedOnArmourType(GameEnums.HeroDefensiveItemType.MasterworkPlateGloves) == GameEnums.HeroDefensiveItemSlotType.Hands,
                 "ArmourStaticGetSlotBasedOnArmourTypeTestTwo (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        [TestMethod]
        public void ArmourStaticGetSlotBasedOnArmourTypeTestThree()
        {
            Assert.IsTrue(Armour.GetSlotTypeBasedOnArmourType(GameEnums.HeroDefensiveItemType.LeatherPants) == GameEnums.HeroDefensiveItemSlotType.Legs,
                 "ArmourStaticGetSlotBasedOnArmourTypeTestThree (ArmourTests) failed. Incorrect Incorrect armour slot type detected.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ArmourIsHeadSlot()
        {
            Armour headArmourPiece = new Armour(GameEnums.HeroDefensiveItemType.MasterworkPlateHelm);

            Assert.IsTrue(headArmourPiece.DefensiveItemTypeSlot == GameEnums.HeroDefensiveItemSlotType.Head, "ArmourIsHeadSlot (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ArmourIsRingSlotTest()
        {
            Armour ringArmourPiece = new Armour(GameEnums.HeroDefensiveItemType.GoldRing);

            Assert.IsTrue(ringArmourPiece.DefensiveItemTypeSlot == GameEnums.HeroDefensiveItemSlotType.Ring, "ArmourIsRingSlotTest (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ArmourIsFeetSlotTest()
        {
            Armour ringArmourPiece = new Armour(GameEnums.HeroDefensiveItemType.MasterworkPlateBoots);

            Assert.IsTrue(ringArmourPiece.DefensiveItemTypeSlot == GameEnums.HeroDefensiveItemSlotType.Feet, "ArmourIsFeetSlotTest (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ArmourIsLeatherChestPieceTest()
        {
            Armour chestArmourPiece = new Armour(GameEnums.HeroDefensiveItemType.LeatherChestPiece);

            Assert.IsTrue(chestArmourPiece.DefensiveItemType == GameEnums.HeroDefensiveItemType.LeatherChestPiece, "ArmourIsLeatherChestPieceTest (ArmourTests) failed. Incorrect armour piece detected.");
            Assert.IsTrue(chestArmourPiece.DefensiveItemTypeSlot == GameEnums.HeroDefensiveItemSlotType.Chest, "ArmourIsLeatherChestPieceTest (ArmourTests) failed. Incorrect armour slot type detected.");
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ArmourIsChainMailHelmTest()
        {
            Armour headArmourPiece = new Armour(GameEnums.HeroDefensiveItemType.ChainMailHelm);

            Assert.IsTrue(headArmourPiece.DefensiveItemType == GameEnums.HeroDefensiveItemType.ChainMailHelm, "ArmourIsLeatherChestPieceTest (ArmourTests) failed. Incorrect armour piece detected.");
            Assert.IsTrue(headArmourPiece.DefensiveItemTypeSlot == GameEnums.HeroDefensiveItemSlotType.Head, "ArmourIsLeatherChestPieceTest (ArmourTests) failed. Incorrect armour slot type detected.");
        }
    }
}