using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Class that represents an enemy (type 'Drone').
/// </summary>
public class EnemyDrone : Enemy
{
    #region Private Data Fields

    //An audio source reference to play the enemy death sound
    private AudioSource audSource = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// Sprites for the walk animation.
    /// </summary>
    public SpriteRenderer[] WalkSprites = null;

    /// <summary>
    /// Sprites for the attack animation.
    /// </summary>
    public SpriteRenderer[] AttackSprites = null;

    /// <summary>
    /// Default sprite (neutral state).
    /// </summary>
    public SpriteRenderer DefaultSprite = null;

    /// <summary>
    /// The clip to play when this enemy is destroyed.
    /// </summary>
    public AudioClip DestroyAudio = null;

    #endregion Unity Inspector Public Variables

    #region Start

    /// <summary>
    /// 
    /// </summary>
    protected override void Start()
    {
        //Debug.Log("In EnemyDrone Start()");

        //Call base start logic
        base.Start();

        //Get the sounds object
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        if (soundsObject == null)
        {
            return; //No death sound
        }

        audSource = soundsObject.GetComponent<AudioSource>();
    }

    #endregion Start

    #region Public Methods

    /// <summary>
    /// Public method that reduces an enemies health based
    /// on the incoming damage.
    /// </summary>
    /// <param name="damage">The amount of damage to apply to this enemy.</param>
    public void Damage(int damage = 0)
    {
        //Reduce health
        Health -= damage;

        //Play damage animation
        gameObject.SendMessage("PlayColourAnimation", 0, SendMessageOptions.DontRequireReceiver);

        //Check if the enemy is dead and post an appropriate notificaton/remove the enemy from the scene
        if (Health <= 0)
        {
            GameManager.Notifications.PostNotification(this, "EnemyDestroyed");
            
            //Play destroyed sound if available
            if (audSource != null)
            {
                audSource.PlayOneShot(DestroyAudio, 1.0f);
            }

            DestroyImmediate(gameObject);

            //Clean up old listeners
            GameManager.Notifications.RemoveRedundancies();
        }
    }

    /// <summary>
    /// Handle the enemies chase state.
    /// </summary>
    public void Chase()
    {
        //Same animations as patrol
        Patrol();
    }

    /// <summary>
    /// Handle an enemies attack state. 
    /// </summary>
    public void Attack()
    {
        //Hide walk sprites
        WalkSprites.ToList().ForEach(sprite => 
        {
            sprite.enabled = false;
        });

        //Hide default sprite
        DefaultSprite.enabled = false;

        //Entered attack state
        SendMessage("StopSpriteAnimation", (int)EnemyState.Patrol, SendMessageOptions.DontRequireReceiver);
        SendMessage("StopSpriteAnimation", (int)EnemyState.Attack, SendMessageOptions.DontRequireReceiver);
        SendMessage("PlaySpriteAnimation", (int)EnemyState.Attack, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Handle the enemies patrol state (i.e. animation).
    /// </summary>
    public void Patrol()
    {
        //Hide attack sprites
        AttackSprites.ToList().ForEach(sprite =>
        {
            sprite.enabled = false;
        });

        //Hide default sprite
        DefaultSprite.enabled = false;

        //Entered patrol state
        SendMessage("StopSpriteAnimation", (int)EnemyState.Patrol, SendMessageOptions.DontRequireReceiver);
        SendMessage("StopSpriteAnimation", (int)EnemyState.Attack, SendMessageOptions.DontRequireReceiver);
        SendMessage("PlaySpriteAnimation", (int)EnemyState.Patrol, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Damage the player.
    /// </summary>
    public void Strike()
    {
        //Damage the player (dunno if the gameObject part of this is actually required)
        playerCont.gameObject.SendMessage("ApplyDamage", AttackDamage, SendMessageOptions.DontRequireReceiver);
    }

    #endregion Public Methods
}
