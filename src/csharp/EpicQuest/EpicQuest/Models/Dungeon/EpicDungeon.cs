using System;
using System.Collections.Generic;
using System.Linq;

namespace EpicQuest.Models.Dungeon
{
    /// <summary>
    /// Public Sealed class that represents a dungeon that adventurers
    /// can 'quest' through with rooms containing things
    /// for the heroes to contend with/collect and interact with.
    /// </summary>
    public sealed class EpicDungeon
    {
        #region Private Data Fields

        //Represents the rooms that make this dungeon up
        private List<Room> dungeonRooms = new List<Room>();

        /// <summary>
        /// Access to the rooms in this Dungeon
        /// (to outside classes).
        /// </summary>
        public List<Room> DungeonRooms
        {
            get
            {
                return dungeonRooms;
            }
            private set
            {
                dungeonRooms = value;
            }
        }

        #endregion Private Data Fields

        #region Constructors

        /// <summary>
        /// Constructor that allows a Dungeon to be created
        /// based on an array of room or comma separated 
        /// room objects (individual instances).
        /// </summary>
        /// <param name="newRooms">Rooms that populate this Dungeon with.</param>
        public EpicDungeon(params Room[] newRooms)
            : this (newRooms.ToList())
        {

        }

        /// <summary>
        /// Constructor that populates this Dungeon with 
        /// the supplied List or Rooms.
        /// </summary>
        /// <param name="newRooms">A List of Rooms to populate this Dungeon with.</param>
        public EpicDungeon(List<Room> newRooms)
        {
            if (newRooms != null && newRooms.Count > 0)
            {
                DungeonRooms = newRooms;
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Public method that allows a Room/Rooms to
        /// be added to this Dungeon.
        /// </summary>
        /// <param name="rooms">A single Room or an array of Rooms to add.</param>
        public void AddDungeonRoom(params Room[] rooms)
        {
            if (rooms != null && rooms.Count() > 0)
            {
                foreach (Room room in rooms)
                {
                    DungeonRooms.Add(room);
                }
            }
        }

        /// <summary>
        /// Public method that allows a Room/Rooms to
        /// be removed from this Dungeon.
        /// </summary>
        /// <param name="rooms">A single Room or an array of Rooms to remove.</param>
        public void RemoveDungeonRoom(params Room[] rooms)
        {
            if (rooms != null && rooms.Count() > 0)
            {
                foreach (Room room in rooms)
                {
                    DungeonRooms.Remove(room);
                }
            }
        }

        #endregion Public Methods

        #region Enumerator

        /// <summary>
        /// Enumerator that allows external classes
        /// to more easily iterate over the Rooms in this
        /// Dungeon (depending on a developers preferences).
        /// </summary>
        /// <returns>The DungeonRooms Enumerator.</returns>
        public List<Room>.Enumerator GetEnumerator()
        {
            return DungeonRooms.GetEnumerator();
        }

        #endregion Enumerator

        #region Public Static Methods

        /// <summary>
        /// Public static helper method that generates a List
        /// of Room objects (based on the seed values provided)
        /// and Room content.
        /// </summary>
        /// <param name="roomAmountSeed">A seed that helps determine the amount of Rooms.</param>
        /// <param name="roomContentSeed">A seed that helps determine the amount of Room Content (in each Room).</param>
        /// <returns>A List of fully populated Rooms based on the seeds specified.</returns>
        public static List<Room> GenerateRandomRoomConfiguration(int roomAmountSeed = 5, int roomContentSeed = 3)
        {
            List<Room> dungeonRooms = new List<Room>();

            //Get a Room count based on the seed
            Random randomRoomCountObj = new Random();
            int thisGameRoomCount = randomRoomCountObj.Next(roomAmountSeed, (roomAmountSeed + 3));

            //Generate Rooms and add Random content
            for (int i = 0; i < thisGameRoomCount; i++)
            {
                dungeonRooms.Add(new Room(Room.GenerateRandomRoomContent(roomContentSeed)));
            }

            return dungeonRooms;
        }

        #endregion Public Static Methods
    }
}