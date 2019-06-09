using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Utility class that maintains a collection of sprite objects as frames
/// of animation. It shows and hides the frames according to a set of playback settings.
/// </summary>
public class SpriteShowAnimator : MonoBehaviour
{
    #region Private Data Fields

    //Indicates if this animation is currently playing
    private bool isPlaying = false;

    #endregion Private Data Fields

    #region Public Enums

    /// <summary>
    /// Playback type. Run once or loop forever.
    /// </summary>
    public enum AnimatorPlaybackType
    {
        PlayOnce,
        PlayLoop
    }

    #endregion Public Enums

    #region Unity Inspector Public Variables

    /// <summary>
    /// Playback type for this animation.
    /// </summary>
    public AnimatorPlaybackType PlaybackType = AnimatorPlaybackType.PlayOnce;

    /// <summary>
    /// Frames per second to play for this animation.
    /// </summary>
    public int Fps = 5;

    /// <summary>
    /// Custom ID for animation. Used with 
    /// method PlaySpriteAnimation.
    /// </summary>
    public int AnimationID = 0;

    /// <summary>
    /// The actual frames of animation.
    /// </summary>
    public SpriteRenderer[] Sprites = null;

    /// <summary>
    /// Should auto-play?
    /// </summary>
    public bool AutoPlay = false;

    /// <summary>
    /// Should first hide all sprite renderers on
    /// playback (or leave as defaults)?
    /// </summary>
    public bool HideSpriteOnStart = true;

    #endregion Unity Inspector Public Variables

    #region Start

    /// <summary>
    /// Start method used for initialisation.
    /// </summary>
	void Start () 
    {
        //Debug.Log("In SpriteShowAnimator Start()");

	    //Should we auto-play on startup
        if (AutoPlay)
        {
            StartCoroutine(PlaySpriteAnimation(AnimationID));
        }
	}

    #endregion Start

    #region Coroutines

    /// <summary>
    /// Coroutine that plays a sprite animation sequentially.
    /// </summary>
    /// <param name="AnimId">The unique ID of the animation to play.</param>
    /// <returns></returns>
    public IEnumerator PlaySpriteAnimation(int AnimId = 0)
    {
        //Check if this animation should be started. Could be called via SendMessage or BroadcastMessage
        if (AnimId != AnimationID)
        {
            yield break;
        }

        //Should hide all sprite renderers
        if (HideSpriteOnStart)
        {
            //Sticking with native array type for performance (plus, below, I can't use yield inside a list foreach using a lambda)
            foreach (SpriteRenderer sr in Sprites)
            {
                sr.enabled = false;
            }
        }

        //Set is playing
        isPlaying = true;

        //Calculate delay time
        float delayTime = 1.0f / Fps;

        //Run animation at least once
        do
        {
            //Lambda using a list extension method foreach not supported (using a standard foreach)
            foreach (SpriteRenderer sr in Sprites)
            {
                sr.enabled = !sr.enabled;

                yield return new WaitForSeconds(delayTime);

                sr.enabled = !sr.enabled;
            }  
        } 
        while (PlaybackType == AnimatorPlaybackType.PlayLoop);

        StopSpriteAnimation(AnimationID);
    }

    #endregion Coroutines

    #region Public Methods

    /// <summary>
    /// Method to stop an animation.
    /// </summary>
    /// <param name="AnimId">The unique id of the animation.</param>
    public void StopSpriteAnimation(int AnimId)
    {
        //Check if this animation can and should be stopped
        if (AnimId != AnimationID || !isPlaying)
        {
            return;
        }

        //Stop all coroutines (animation will no longer play)
        StopAllCoroutines();

        //Animation no longer playing at this point
        isPlaying = false;

        //Send sprite animation stopped event to the game object
        gameObject.SendMessage("SpriteAnimationStopped", AnimId, SendMessageOptions.DontRequireReceiver);
    }

    #endregion Public Methods
}
