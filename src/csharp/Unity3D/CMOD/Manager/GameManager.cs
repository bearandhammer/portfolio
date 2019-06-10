using UnityEngine;
using System.Collections;

/// <summary>
/// CMOD Core Game Manager class (singleton).
/// </summary>
[RequireComponent(typeof(NotificationsManager))]        //Component for sending and receiving event notifications (will be a singleton), added by Unity if missing
public class GameManager : MonoBehaviour
{
    #region Private Static Data Fields

    //Static singleton GameManager instance (to be used by all other objects during gameplay)
    private static GameManager instance = null;
    
    //Static singleton NotificationsManager instance (to be used for event posting/receiving by all other objects during gameplay)
    private static NotificationsManager notifications = null;

    //Static singleton that handles load/save states/functionality
    private static LoadSaveManager stateManager = null;

    //Static variable - Should load from save game state on level load, or just restart level from defaults
    private static bool bShouldLoad = false;

    #endregion Private Static Data Fields

    #region Private Data Fields

    //Is input allowed (from the user) at this given time
    private bool inputAllowed = true;

    //Audio source for playing the game music
    //private AudioSource audSource = null;

    #endregion Private Data Fields

    #region Public Static Properties

    /// <summary>
    /// Provide public read access to the singleton GameManager instance
    /// and create a singular instance if required.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Provide public read access to the singleton NotificationsManager instance
    /// and create a singular instance if required (all other classes will use this 
    /// for event posting/receiving).
    /// </summary>
    public static NotificationsManager Notifications
    {
        get
        {
            if (notifications == null)
            {
                notifications = instance.GetComponent<NotificationsManager>();
            }

            return notifications;
        }
    }

    /// <summary>
    /// Provide public read access to the singleton LoadSaveManager instance
    /// and create a singular instance if required (all other classes will use this
    /// for load/save functionality).
    /// </summary>
    public static LoadSaveManager StateManager
    {
        get
        {
            if (stateManager == null)
            {
                stateManager = Instance.GetComponent<LoadSaveManager>();
            }

            return stateManager;
        }
    }

    #endregion Public Static Properties

    #region Public Properties

    /// <summary>
    /// Public access to the inputAllowed data field (can user input be accepted). 
    /// When this is changed a notification is posted to engaged any objects that need
    /// to know about this kind of event.
    /// </summary>
    public bool InputAllowed
    {
        get
        {
            return inputAllowed;
        }
        set
        {
            inputAllowed = value;
            Notifications.PostNotification(this, "InputChanged");
        }
    }

    #endregion Public Properties

    #region Unity Inspector Public Variables

    /// <summary>
    /// Audio clip for the game music.
    /// </summary>
    public AudioClip GameMusic = null;

    #endregion Unity Inspector Public Variables

    #region Awake/Start

    /// <summary>
    /// On Awake, check to see if this newly created instance conflicts with
    /// a singular, existing instance. If it does do not allow this object to be 
    /// created (as we only ever want one GameManager in play at any given time).
    /// </summary>
    void Awake()
    {
        //Debug.Log(string.Concat("Calling Awake() on GameManager. InstanceID: " + GetInstanceID()));

        //If there is an existing instance and the newly created instance does not match then remove this instance
        if (instance != null && instance.GetInstanceID() != GetInstanceID())
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            //This is the one and only, correct instance (do not destroy this object)
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// GameManager initialisation logic.
    /// </summary>
    void Start()
    {
        //Debug.Log("In GameManager Start()");

        //Add cash collected listener to listen for win condition
        GameManager.Notifications.AddListener(this, "CashCollected");

        //Listen for menu events
        GameManager.Notifications.AddListener(this, "RestartGame");
        GameManager.Notifications.AddListener(this, "ExitGame");
        GameManager.Notifications.AddListener(this, "SaveGame");
        GameManager.Notifications.AddListener(this, "LoadGame");
        GameManager.Notifications.AddListener(this, "LoadGameComplete");

        //GameManager.Notifications.OnLevelWasLoaded();

        //If we need to load level
        if (bShouldLoad)
        {
            //Debug.Log(string.Concat("Loading from: ", Application.persistentDataPath));
            StateManager.Load(string.Concat(Application.persistentDataPath, "/SaveGame.xml"));
            bShouldLoad = false; //Reset load flag
        }

        //Attempt to set the reference so that the game music audio can be played
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        if (soundsObject == null)
        {
            return;
        }

        soundsObject.GetComponent<AudioSource>().PlayOneShot(GameMusic, 0.5f);
    }

    #endregion Awake/Start

    #region Public Methods

    /// <summary>
    /// When the cash is collected for this level
    /// then the player has completed the level.
    /// </summary>
    /// <param name="sender">The sender component (a cash prefab).</param>
    public void CashCollected(Component sender)
    {
        //Disable input
        InputAllowed = false;

        //Pause game
        Time.timeScale = 0;

        //Show level complete sign
        GameObject.Find("Sprite_Mission_Complete").GetComponent<SpriteRenderer>().enabled = true;
    }

    /// <summary>
    /// Restart the game.
    /// </summary>
    public void RestartGame()
    {
        //Notifications.RemoveRedundancies();
        Application.LoadLevel(0); //Application.loadedLevel
    }

    /// <summary>
    /// Exit game method (to simply quit).
    /// </summary>
    public void ExitGame()
    {
        //Quit the game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Save game state.
    /// </summary>
    public void SaveGame()
    {
        //Notifications.RemoveRedundancies();
        StateManager.Save(string.Concat(Application.persistentDataPath, "/SaveGame.xml"));
    }

    /// <summary>
    /// Load game state.
    /// </summary>
    public void LoadGame()
    {
        //Notifications.RemoveRedundancies();

        //Set load on restart
        bShouldLoad = true;

        //Restart level
        RestartGame();
    }

    #endregion Public Methods
}