using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Represents the hand gun weapon in CMOD.
/// </summary>
public class WeaponGun : Weapon 
{
    #region Private Data Fields

    //Audio Source for sound playback
    private AudioSource audSource = null;

    //Reference to all child sprite renderers for this weapon
    private SpriteRenderer[] weaponSprites = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    //Default Sprite to show for the weapon when active and not attacking
    public SpriteRenderer DefaultSprite = null;

    //Sound to play on attack
    public AudioClip WeaponAudio = null;

    #endregion Unity Inspector Public Variables

    #region Start/Update

    //NOT THE BEST CODE REUSE IN THIS CLASS - TO REVISIT

	/// <summary>
    /// Start method for initialisation.
	/// </summary>
	void Start () 
    {
        //Debug.Log("In WeaponGun Start()");

        weaponSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();

        //Just get the first SpriteRenderer and assume it's the default??? Alternative approach...
        //DefaultSprite = weaponSprites[0];

        //Register weapon for weapon change events
        GameManager.Notifications.AddListener(this, "WeaponChange");

        //Long-handed - Would probably reduce myself (but keeping to book content). Find sound object in scene
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        if (soundsObject == null)
        {
            return;
        }

        //Get audio source component for sfx
        audSource = soundsObject.GetComponent<AudioSource>();    
	}
	
	// Update is called once per frame
	void Update () 
    {
        //If the weapon is not equipped or input is not allowed then return
        if (!IsEquipped || !GameManager.Instance.InputAllowed)
        {
            return;
        }

        //Coroutine to fire the weapon (will manipulate CanFire)
        if (Input.GetButton("Fire1") && CanFire)
        {
            StartCoroutine(Fire());
        }	
	}

    #endregion Start/Update

    #region Public Methods

    /// <summary>
    /// Called (from the SpriteShowAnimator class) when an 
    /// attack animation has completed playback.
    /// </summary>
    public void SpriteAnimationStopped()
    {
        if (!IsEquipped)
        {
            return;
        }

        //Show default sprite (i.e. the non-attack sprite show for the weapon when simply mulling around the scene)
        DefaultSprite.enabled = true;
    }

    /// <summary>
    /// Allow this weapon to be equipped.
    /// </summary>
    /// <param name="WeapType">The callers requested weapon type. Must match this weapons type in order to be equipped.</param>
    /// <returns>True if the weapon can be equipped. Otherwise, false.</returns>
    public bool Equip(WeaponType WeapType)
    {
        //If the weapon type does not match this weapon, the weapon is not collected, the weapon has no ammo or the weapon is already equipped then simply return false
        if (WeapType != Type || !Collected || Ammo == 0 || IsEquipped)
        {
            return false; //Get the next weapon
        }

        //Is this weapon so equip
        IsEquipped = true;

        //Show default sprite
        DefaultSprite.enabled = true;

        //Activate CanFire ready for a players first attack
        CanFire = true;

        //Send weapon change event
        GameManager.Notifications.PostNotification(this, "WeaponChange");

        //Weapon was equipped - return true
        return true;
    }

    /// <summary>
    /// Called when the player changes weapon.
    /// </summary>
    /// <param name="sender">Weapon that triggered the change event.</param>
    public void WeaponChange(Component sender)
    {
        //Has the player changed to this weapon. We can only change to a 'different' weapon
        if (sender.GetInstanceID() == GetInstanceID())
        {
            return;
        }

        //Has changed to other weapon. Hide this weapon
        StopAllCoroutines();
        gameObject.SendMessage("StopSpriteAnimation", 0, SendMessageOptions.DontRequireReceiver);

        //Deactivate equipped for this weapon and hide all sprites
        IsEquipped = false;

        weaponSprites.ToList().ForEach(sr =>
        {
            sr.enabled = false;
        });
    }

    #endregion Public Methods

    #region Coroutines

    /// <summary>
    /// Represents a coroutine to fire the weapon.
    /// </summary>
    /// <returns>WaitForSeconds denoting the time to wait between attacks.</returns>
    public IEnumerator Fire()
    {
        //Check to see if we can fire (backup check)
        if (!CanFire || !IsEquipped || Ammo <= 0)
        {
            yield break;
        }

        //Set refire to false
        CanFire = false;

        //Play Fire animation (sending a message to the SpriteShowAnimator class coroutine associated with this object)
        gameObject.SendMessage("PlaySpriteAnimation", 0, SendMessageOptions.DontRequireReceiver);

        //Play attack sound. if the audio source is available
        if (audSource != null)
        {
            audSource.PlayOneShot(WeaponAudio, 1.0f);
        }

        //Calculate hit. Get ray from screen centre target and test for a hit with an 'enemy' (the actual beef of what we want to do!)
        Ray rayObj = Camera.main.ScreenPointToRay(new Vector3((Screen.width / 2), (Screen.height / 2), 0.0f));

        //Debug.DrawRay(rayObj.origin, rayObj.direction, Color.red, 10.0f);

        RaycastHit hit;
        if (Physics.Raycast(rayObj.origin, rayObj.direction, out hit, Range))
        {
            //UNCOMMENT THIS WHEN ENEMIES ARE CREATED
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                //Send damage message (deal damage to the enemy
                hit.collider.gameObject.SendMessage("Damage", Damage, SendMessageOptions.DontRequireReceiver);
            }
        }

        //Reduce Ammo
        --Ammo;

        //Wait for recovery before re-enabling CanFire (MOVE ABOVE AMMO CHECK??? - TO TEST (MIGHT FIX AN ANIMATION PROBLEM IM HAVING)) - KEEPING FOR NOW, SEEMS TO WORK
        yield return new WaitForSeconds(RecoveryDelay);

        //Check ammo and post an empty notification if the player has run out
        if (Ammo <= 0)
        {
            GameManager.Notifications.PostNotification(this, "AmmoExpired");
        }

        //Re-enable CanFire
        CanFire = true;
    }

    #endregion Coroutines
}
