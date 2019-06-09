using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Class representing a tough guy enemy.
/// </summary>
public class EnemyToughGuy : Enemy
{
    #region Private Data Fields

    private AudioSource audSource = null; 

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// The audio clip to play when this enemy is destroyed.
    /// </summary>
    public AudioClip DestroyAudio = null;

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

    #endregion Unity Inspector Public Variables

    #region Start

    /// <summary>
    /// Overriden start method (calls Toughguy specific functionality
    /// as well as the parent enemy class Start method).
    /// </summary>
    protected override void Start()
    {
        //Call the base Start() method functionality
        base.Start();

        //Find the sounds object
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        //If no sound object, then exit
        if (soundsObject == null)
        {
            return;
        }

        //Get the audio source component for local use
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
    /// Handles the enemies patrol state (i.e. animation).
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
    /// Handles the enemies patrol state.
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
    /// Handle an enemies strike on the player.
    /// </summary>
    public void Strike()
    {
        //Damage player
        playerCont.gameObject.SendMessage("ApplyDamage", AttackDamage, SendMessageOptions.DontRequireReceiver);
    }

    #endregion Public Methods
}