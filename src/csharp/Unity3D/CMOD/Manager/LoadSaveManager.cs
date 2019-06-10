using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

/// <summary>
/// Loads and saves game state data.
/// </summary>
public class LoadSaveManager : MonoBehaviour
{
    #region GameStateData Class

    /// <summary>
    /// GameStateData class (stores entities persistent data).
    /// </summary>
    [XmlRoot("GameData")]
    public class GameStateData
    {
        /// <summary>
        /// Struct representing an objects transform data.
        /// </summary>
        public struct DataTransform
        {
            public float X;
            public float Y;
            public float Z;
            public float RotX;
            public float RotY;
            public float RotZ;
            public float ScaleX;
            public float ScaleY;
            public float ScaleZ;
        }

        /// <summary>
        /// An enemies persistent data.
        /// </summary>
        public class DataEnemy
        {
            public DataTransform PosRotScale;
            public int EnemyID;
            public int Health;
        }

        /// <summary>
        /// The players persistent data.
        /// </summary>
        public class DataPlayer
        {
            public DataTransform PosRotScale;
            public float CollectedCash;
            public bool CollectedGun;
            public int Health;
        }

        /// <summary>
        /// Reference to all enemies (persistent data list).
        /// </summary>
        public List<DataEnemy> Enemies = new List<DataEnemy>();

        /// <summary>
        /// Refererence to the players persistent data.
        /// </summary>
        public DataPlayer Player = new DataPlayer();
    }

    #endregion GameStateData Class

    #region Public Variables

    /// <summary>
    /// An instance to save GameState.
    /// </summary>
    public GameStateData GameState = new GameStateData();

    #endregion Public Variables

    #region Public Methods

    /// <summary>
    /// Save game data.
    /// </summary>
    /// <param name="fileName">The xml file name to save data to (GameData.xml by default).</param>
    public void Save(string fileName = "GameData.xml")
    {
        //Clear existing enemy data
        GameState.Enemies.Clear();

        //Call save start notification
        GameManager.Notifications.PostNotification(this, "SaveGamePrepare");

        //Save game data (debug.log any exceptions for now, needs better handling in future)
        XmlSerializer serialiser = new XmlSerializer(typeof(GameStateData));

        try
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Create))
            {
                serialiser.Serialize(stream, GameState);
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

        //Call save end notification
        GameManager.Notifications.PostNotification(this, "SaveGameComplete");
    }

    /// <summary>
    /// Load game data.
    /// </summary>
    /// <param name="fileName">The xml file name to load data from (GameData.xml by default).</param>
    public void Load(string fileName = "GameData.xml")
    {
        GameManager.Notifications.PostNotification(this, "LoadGamePrepare");

        //Load game state state data from xml (debug log exceptions, requires better handling)
        XmlSerializer serialiser = new XmlSerializer(typeof(GameStateData));

        try
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open))
            {
                GameState = serialiser.Deserialize(stream) as GameStateData;
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
        
        //Call load end notification
        GameManager.Notifications.PostNotification(this, "LoadGameComplete");
    }

    #endregion Public Variables
}
