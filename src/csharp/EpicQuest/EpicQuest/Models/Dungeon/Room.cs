using EpicQuest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EpicQuest.Models.Dungeon
{
    /// <summary>
    /// Public Sealed class that represents the Rooms
    /// in a Dungeon (filled with monsters and treasure).
    /// </summary>
    public sealed class Room : IUniqueItem
    {
        #region Private Data Fields

        //A Rooms unique reference (should a room need to be uniquely identified) and a List of Room Content
        private readonly Guid uniqueRef = Guid.NewGuid();
        private List<IRoomContent> roomFeatures = new List<IRoomContent>();

        #endregion Private Data Fields

        #region Properties

        /// <summary>
        /// Access to this Rooms Unique Reference.
        /// </summary>
        public Guid UniqueRef
        {
            get
            {
                return uniqueRef;
            }
        }

        /// <summary>
        /// Helper property that checks to see if any 
        /// remaining Room Content is of type Monster.
        /// </summary>
        public bool RoomClearedOfMonsters
        {
            get
            {
                if (roomFeatures.Any(feature => feature is Monster))
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// Access to a Rooms Content Features.
        /// </summary>
        public List<IRoomContent> RoomFeatures 
        { 
            get
            {
                return roomFeatures;
            }
            private set
            {
                if (value != null && value.Count > 0)
                {
                    roomFeatures = value;
                }
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Room constructor that allows a Room to be populated
        /// with a single piece of content (or an array of content).
        /// </summary>
        /// <param name="features">The content to place in the Room (single piece or an array of).</param>
        public Room(params IRoomContent[] features)
            : this (features.ToList())
        {

        }

        /// <summary>
        /// Room constructor that allows a Room to be populated
        /// with a List of content.
        /// </summary>
        /// <param name="features">A list of content to place in the Room.</param>
        public Room(List<IRoomContent> features)
        {
            if (features != null && features.Count > 0)
            {
                RoomFeatures = features;
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Public method that allows a piece of content/multiple pieces
        /// of content to be added to this particular Room.
        /// </summary>
        /// <param name="features">A single piece of content/array of content pieces to add to the Room.</param>
        public void AddContentToRoom(params IRoomContent[] features)
        {
            if (features != null && features.Count() > 0)
            {
                foreach (IRoomContent item in features)
                {
                    roomFeatures.Add(item);
                }
            }
        }

        /// <summary>
        /// Public method that allows a piece of content/multiple pieces
        /// of content to be removed from this particular Room.
        /// </summary>
        /// <param name="features">>A single piece of content/array of content pieces to removed from the Room.</param>
        public void RemoveContentFromRoom(params IRoomContent[] features)
        {
            if (features != null && features.Count() > 0)
            {
                foreach (IRoomContent item in features)
                {
                    roomFeatures.Remove(item);
                }
            }
        }

        #endregion Public Methods

        #region Enumerator

        /// <summary>
        /// Enumerator that allows external classes
        /// to more easily iterate over the Room Features in this
        /// Room (depending on a developers preferences).
        /// </summary>
        /// <returns>The RoomFeatures Enumerator.</returns>
        public List<IRoomContent>.Enumerator GetEnumerator()
        {
            return RoomFeatures.GetEnumerator();
        }

        #endregion Enumerator

        #region Public Static Methods

        /// <summary>
        /// Public static helper method that generates and returns a random
        /// List of Room Content.
        /// </summary>
        /// <param name="contentAmountSeed">A seed to help determine the amount of pieces to generate.</param>
        /// <returns>A random List of Room Content.</returns>
        public static List<IRoomContent> GenerateRandomRoomContent(int contentAmountSeed = 3)
        {
            //CURRENTLY HARD CODE TO RETURN TWO MONSTERS AS CONTENT (COMBAT TEST)
            return new List<IRoomContent>(new IRoomContent[] { new Kobold(), new Skeleton() });
        }

        #endregion Public Static Methods
    }
}