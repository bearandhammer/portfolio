using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EpicQuest.Models.Dungeon;
using EpicQuest.Interfaces;
using EpicQuest.Models;
using System.Collections.Generic;
using System.Linq;
using EpicQuest.Models.Items;

namespace EpicQuest.Tests.Rooms
{
    /// <summary>
    /// Room entity specific Unit Tests
    /// for the Epic Quest game.
    /// </summary>
    [TestClass]
    public class RoomTests
    {
        /// <summary>
        /// Test method that ensures that the Room
        /// object (static) level GenerateRandomRoomContent method 
        /// is working as expected. 
        /// </summary>
        [TestMethod]
        public void RoomContentGenerationTest()
        {
            //Test that Room.GenerateRandomRoomContent is producing content based on the seed provided
            int roomContentSeed = 2, roomContentNumber = Room.GenerateRandomRoomContent(roomContentSeed).Count, difference = (roomContentNumber - roomContentSeed);

            //Produce some extra debugging output
            System.Diagnostics.Trace.WriteLine(string.Format("RoomContentGenerationTest (RoomTests) roomContentSeed {0}; roomContentNumber: {1}; difference: {2}.", 
                roomContentSeed, roomContentNumber, difference));

            //Ensure the room content number produced is within the expected range (based on the seed)
            if (difference < 0 || difference > 2)
            {
                Assert.Fail("RoomContentGenerationTest (RoomTests) failed. Room content difference between seed number and actual, generated number out of range. Difference: {0}", difference);
            }
        }

        /// <summary>
        /// Test method that ensures that the Room
        /// object constructor is working as expected.
        /// </summary>
        [TestMethod]
        public void RoomContentCreationTestOne()
        {
            //Add some content to a list of type IRoomContent and pass this to the Room constructor
            List<IRoomContent> roomContentList = new List<IRoomContent>();
            roomContentList.AddRange(new IRoomContent[] { new Skeleton(), new Skeleton(), new Kobold(), new Kobold() });

            Room room = new Room(roomContentList);

            //Check to ensure that the content in the newly created Room object is based on the information passed to the constructor
            Assert.AreEqual(roomContentList, room.RoomFeatures, "RoomContentCreationTestOne (RoomTests) failed. Test room content array is not represented by room.RoomFeatures (different refs).");
        }

        /// <summary>
        /// Test method that ensures that the Room
        /// object constructor is working
        /// as expected (very basic test).
        /// </summary>
        [TestMethod]
        public void RoomContentCreationTestTwoBasic()
        {
            //Chuck an array of IRoomContent to the Room constructor
            IRoomContent[] roomContent = new IRoomContent[] { new Kobold(), new Skeleton() };

            Room room = new Room(roomContent);

            //Again, similar to the Dungeon test of a similiar nature, do a simple count of content items and ensure it's correct
            Assert.AreEqual<int>(roomContent.Length, room.RoomFeatures.Count, "RoomContentCreationTestTwoBasic (RoomTests) failed. Test array room Content count differs to room.RoomFeatures.Count.");
        }

        /// <summary>
        /// Test method that ensures that the Room
        /// instance level AddContentToRoom method is working
        /// as expected.
        /// </summary>
        [TestMethod]
        public void RoomContentAdditionTest()
        {
            //Create a new room and added random content (as base content). Take a note of the items of content currently in the room
            Room room = new Room(Room.GenerateRandomRoomContent());

            int currentRoomFeatureCount = room.RoomFeatures.Count;

            //Add a new monster to the room
            IRoomContent kobold = new Kobold(); 
            room.AddContentToRoom(kobold);

            //Ensure the monster has correctly been added to the room
            Assert.IsTrue(room.RoomFeatures.Contains(kobold), "RoomContentAdditionTest (RoomTests) failed. Test room feature kobold not found in room.RoomFeatures.");
            Assert.AreEqual<int>((currentRoomFeatureCount + 1), room.RoomFeatures.Count, "RoomContentAdditionTest (RoomTests) failed. Room feature count (room.RoomFeatures.Count) has an unexpected value.");
        }

        /// <summary>
        /// Test method that ensures that the Room
        /// instance level RemoveContentFromRoom method is working
        /// as expected. 
        /// </summary>
        [TestMethod]
        public void RoomContentRemovalTest()
        {
            //Add a new skeleton to a new room (with some other random content to strengthen the test example). Take a note of the count of room features prior to calling RemoveContentFromRoom
            IRoomContent skeleton = new Skeleton();

            Room room = new Room(Room.GenerateRandomRoomContent());
            room.AddContentToRoom(skeleton);

            int previousRoomFeatureCount = room.RoomFeatures.Count;

            //Run the test, remove the skeleton from the room
            room.RemoveContentFromRoom(skeleton);

            //Ensure that the known skeleton instance has been removed from the room (and that the count of remaining pieces of room content is correct)
            Assert.IsFalse(room.RoomFeatures.Contains(skeleton), "RoomContentRemovalTest (RoomTests) failed. Test room feature skeleton still found in room.RoomFeatures.");
            Assert.AreEqual<int>((previousRoomFeatureCount - 1), room.RoomFeatures.Count, "RoomContentRemovalTest (RoomTests) failed. Room feature count (room.RoomFeatures.Count) has an unexpected value.");
        }

        /// <summary>
        /// Test method that ensures that the Room
        /// instance level RoomClearedOfMonstersTest method 
        /// is working as expected. 
        /// </summary>
        [TestMethod]
        public void RoomClearedOfMonstersTest()
        {
            //Configure some fixed, known pieces of room content and add it to a new room
            IRoomContent skeleton = new Skeleton(), kobold = new Kobold(), treasureItem = new TreasureItem();

            Room room = new Room(skeleton, kobold, treasureItem);

            //Test that the room contains monsters at this point
            Assert.IsFalse(room.RoomClearedOfMonsters, "RoomClearedOfMonstersTest (RoomTests) failed. No monsters detected in the room.");

            //Remove just the fixed, known monsters - Leaving the treasure item intact
            room.RemoveContentFromRoom(skeleton, kobold);

            //Final round of tests. Ensure all monsters have been removed and that only one piece of content remains (the treasure item)
            Assert.IsTrue(room.RoomClearedOfMonsters, "RoomClearedOfMonstersTest (RoomTests) failed. Monsters detected in the room.");
            Assert.AreEqual<int>(room.RoomFeatures.Count, 1, "RoomClearedOfMonstersTest (RoomTests) failed. An unexpected room feature count was detected (room.RoomFeatures.Count).");
            Assert.IsInstanceOfType(room.RoomFeatures.First(), typeof(TreasureItem), "RoomClearedOfMonstersTest (RoomTests) failed. Remaining room feature is not of the expected type. Type detected: {0}",
                room.RoomFeatures.First().GetType().Name);
        }
    }
}
