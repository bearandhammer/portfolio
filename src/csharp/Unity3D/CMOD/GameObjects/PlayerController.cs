using UnityEngine;
using System.Collections;

/// <summary>
/// CMOD Player Controller class that handles player
/// centric logic.
/// </summary>
public class PlayerController : MonoBehaviour
{
    #region Private Data Fields

    //The players current cash count
    private float cash = 0.0f;

    //Get Mecanim animator for the camera death animation
    private Animator animComp = null;

    //Texture to indicate damage to the player
    private Texture2D damageTexture = null;

    //Screen co-ordinates
    private Rect screenRect;

    //Show the damage texture?
    private bool showDamage = false;

    //Damage texture interval (amount of time in seconds to show texture)
    private float damageInterval = 0.2f;

    //Cached transform for this object (for further user later)
    private Transform thisTransform = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// The players health amount. Weird one here, book wants this
    /// surfaced in the Unity Inspector but I may change at the end
    /// (as logic is embedded in the property Health on this class that might
    /// be bypassed by this being public).
    /// </summary>
    public int HealthAmount = 100;

    /// <summary>
    /// Represents the total cash (in the level) - Limit that needs to be reached to complete the level.
    /// </summary>
    public float CashTotal = 1400.0f;

    /// <summary>
    /// Respawn time in seconds after dying.
    /// </summary>
    public float RespawnTime = 2.0f;

    /// <summary>
    /// Default player weapon (Punch).
    /// </summary>
    public Weapon DefaultWeapon = null;

    /// <summary>
    /// Currently active weapon.
    /// </summary>
    public Weapon ActiveWeapon = null;

    /// <summary>
    /// All input components to be enable/disable based on game events.
    /// </summary>
    public MonoBehaviour[] FPSInputComponents = null;

    /// <summary>
    /// A reference to the collectible weapon (Gun).
    /// </summary>
    public Weapon CollectWeapon = null;

    #endregion Unity Inspector Public Variables

    #region Public Properties

    /// <summary>
    /// Public accessor for this players cash amount.
    /// Posts a 'CashCollected' notification when the player
    /// cash count is greater than or equal to the CashTotal required
    /// to complete the level.
    /// </summary>
    public float Cash 
    {
        get 
        {
            return cash;
        }
        set 
        {
            cash = value;

            //Win condition - Required cash limit reached to complete the level
            if (Cash >= CashTotal)
            {
                GameManager.Notifications.PostNotification(this, "CashCollected");
            }
        }
    }

    /// <summary>
    /// Public accessor for the Players health. If player health
    /// falls below zero then the player is dead.
    /// </summary>
    public int Health
    {
        get
        {
            return HealthAmount;
        }
        set
        {
            HealthAmount = value;

            //Player death logic
            if (Health <= 0)
            {
                gameObject.SendMessage("Die", SendMessageOptions.DontRequireReceiver); //Original code - Just in case, maybe the Die method exists further times on this object later
                //StartCoroutine(Die()); //Why not just start a Coroutine??? Will read into it further
            }
        }
    }

    #endregion Public Properties

    #region Start/Update/OnGUI

    /// <summary>
	/// Start logic for the PlayerController (get this objects transform
    /// and hide the capsule mesh for the player in-game, etc).
	/// </summary>
	void Start () 
    {
        //Turn of the 'rendered' capsule for the controller so it can't be seen in the scene (does not affect collisions, just mesh visibility)
        MeshRenderer capsule = GetComponentInChildren<MeshRenderer>();
        capsule.enabled = false;

        //Register controller for weapon expiration events and load/save game events
        GameManager.Notifications.AddListener(this, "AmmoExpired");
        GameManager.Notifications.AddListener(this, "SaveGamePrepare");
        GameManager.Notifications.AddListener(this, "LoadGameComplete");

        //Register controller for input change events
        GameManager.Notifications.AddListener(this, "InputChanged");

        //Activate default weapon
        DefaultWeapon.gameObject.SendMessage("Equip", DefaultWeapon.Type);

        //Set the active weapon
        ActiveWeapon = DefaultWeapon;

        //Get death camera animator
        animComp = GetComponentInChildren<Animator>();

        //Create damage texture
        damageTexture = new Texture2D(1, 1);
        damageTexture.SetPixel(0, 0, new Color(255f, 0f, 0f, 0.5f));
        damageTexture.Apply();

        //Get cached transform (just once for performance reasons)
        thisTransform = transform;
    }

    /// <summary>
    /// Update method called to keep the screenRect in
    /// check (for the damage texture to be displayed properly).
    /// </summary>
    void Update()
    {
        screenRect.x = screenRect.y = 0;
        screenRect.width = Screen.width;
        screenRect.height = Screen.height;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipNextWeapon();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.CaptureScreenshot(Application.persistentDataPath + @"\screenshot.png");
        }
    }

    /// <summary>
    /// Shows UI elements relating to the player object.
    /// </summary>
    void OnGUI()
    {
        //If damaged then show the damage texture
        if (showDamage)
        {
            GUI.DrawTexture(screenRect, damageTexture);
        }
    }

    #endregion Start/OnGUI

    #region Public Methods

    /// <summary>
    /// Equip the next available weapon.
    /// </summary>
    public void EquipNextWeapon()
    {
        bool bFoundWeapon = false;

        //Loop until weapon foudn
        while(!bFoundWeapon)
        {
            //Get next weapon
            ActiveWeapon = ActiveWeapon.NextWeapon;

            //Activate weapon, if possible
            ActiveWeapon.gameObject.SendMessage("Equip", ActiveWeapon.Type);

            //Is successfully equipped
            bFoundWeapon = ActiveWeapon.IsEquipped;
        }
    }

    /// <summary>
    /// If a weapons ammo expires then equip the next weapon.
    /// </summary>
    /// <param name="sender">Component gun object.</param>
    public void AmmoExpired(Component sender)
    {
        //Ammo expired for this weapon. Equip next
        EquipNextWeapon();
    }
    
    /// <summary>
    /// When input status changes enable/disable FPS components.
    /// </summary>
    /// <param name="sender">The event sending component.</param>
    public void InputChanged(Component sender)
    {
        bool inputStatus = GameManager.Instance.InputAllowed;

        foreach (MonoBehaviour mb in FPSInputComponents)
        {
            mb.enabled = inputStatus;
        }
    }

    /// <summary>
    /// Log player game state data (in preparation for save).
    /// </summary>
    /// <param name="sender">The component sender object.</param>
    public void SaveGamePrepare(Component sender)
    {
        //Get player data object
        LoadSaveManager.GameStateData.DataPlayer playerData = GameManager.StateManager.GameState.Player;

        //Fill in player data for save game
        playerData.CollectedCash = Cash;
        playerData.CollectedGun = CollectWeapon.Collected;
        playerData.Health = Health;
        playerData.PosRotScale.X = thisTransform.position.x;
        playerData.PosRotScale.Y = thisTransform.position.y;
        playerData.PosRotScale.Z = thisTransform.position.z;
        playerData.PosRotScale.RotX = thisTransform.localEulerAngles.x;
        playerData.PosRotScale.RotY = thisTransform.localEulerAngles.y;
        playerData.PosRotScale.RotZ = thisTransform.localEulerAngles.z;
        playerData.PosRotScale.ScaleX = thisTransform.localScale.x;
        playerData.PosRotScale.ScaleY = thisTransform.localScale.y;
        playerData.PosRotScale.ScaleZ = thisTransform.localScale.z;
    }

    /// <summary>
    /// Load player state information.wwws
    /// </summary>
    /// <param name="sender">The component sender object.</param>
    public void LoadGameComplete(Component sender)
    {
        //Get player data object
        LoadSaveManager.GameStateData.DataPlayer playerData = GameManager.StateManager.GameState.Player;

        //Load data back into the player object
        Cash = playerData.CollectedCash;

        //Give player weapon, active and destroy weapon power-upwwwwwwws
        if (playerData.CollectedGun)
        {
            //Find weapon powerup in level
            GameObject.Find("Prefab_Weapon_Powerup").SendMessage("OnTriggerEnter", GetComponent<Collider>(), SendMessageOptions.DontRequireReceiver);
        }

        //Set health
        Health = playerData.Health;

        //Set position, rotation and scale
        thisTransform.position = new Vector3(playerData.PosRotScale.X, playerData.PosRotScale.Y, playerData.PosRotScale.Z);
        thisTransform.localRotation = Quaternion.Euler(playerData.PosRotScale.RotX, playerData.PosRotScale.RotY, playerData.PosRotScale.RotZ);
        thisTransform.localScale = new Vector3(playerData.PosRotScale.ScaleX, playerData.PosRotScale.ScaleY, playerData.PosRotScale.ScaleZ);
    }

    #endregion Public Methids

    #region Coroutines

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator ApplyDamage(int amount = 0)
    {
        //Reduce health
        Health -= amount;

        //Post damage notification
        GameManager.Notifications.PostNotification(this, "PlayerDamaged");

        //Show damage texture, wait based on the interval then hide the damage texture
        showDamage = true;

        yield return new WaitForSeconds(damageInterval);

        showDamage = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IEnumerator Die()
    {
        //Disable input
        GameManager.Instance.InputAllowed = false;

        //Show the death animation if it's set
        if (animComp != null)
        {
            animComp.SetTrigger("ShowDeath");
        }

        //Wait for the respawn timer
        yield return new WaitForSeconds(RespawnTime);

        //Restart level
        Application.LoadLevel(Application.loadedLevel);
    }

    #endregion Coroutines
}