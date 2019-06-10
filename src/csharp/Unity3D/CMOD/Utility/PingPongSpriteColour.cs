using UnityEngine;
using System.Collections;

/// <summary>
/// Sets colour for all child sprite renderers in a gameobject.
/// </summary>
public class PingPongSpriteColour : MonoBehaviour
{
    #region Private Data Fields

    //Represents child sprite renderer objects (i.e. animation textures)
    private SpriteRenderer[] spriteRenderers = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// Source (from) colour.
    /// </summary>
    public Color Source = Color.white;

    /// <summary>
    /// Destination (to) colour.
    /// </summary>
    public Color Dest = Color.white;

    /// <summary>
    /// Custom ID for this animation.
    /// </summary>
    public int AnimationID = 0;

    /// <summary>
    /// Total time in seconds to transition from source to dest.
    /// </summary>
    public float TransitionTime = 1.0f;

    #endregion Unity Inspector Public Variables

    #region Start

    /// <summary>
    /// PingPongSpriteColour initialisation.
    /// </summary>
    void Start()
    {
        //Debug.Log("In PingPongSpriteColour Start()");

        spriteRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    #endregion Start

    #region Public Methods

    /// <summary>
    /// Start the animation (giving damage feedback for enemies).
    /// </summary>
    /// <param name="AnimID">This animations id.</param>
    public void PlayColourAnimation(int animId = 0)
    {
        //If animation id numbers do not match then return
        if (AnimationID != animId)
        {
            return;
        }

        //Stop all running coroutines and start a new sequence
        StopAllCoroutines();
        StartCoroutine(PlayLerpColours());
    }

    #endregion Public Methods

    #region Coroutines

    /// <summary>
    /// Start damage feedback animation.
    /// </summary>
    /// <returns>IEnumerator controlling the damage feedback animation.</returns>
    private IEnumerator PlayLerpColours()
    {
        //Lerp colours
        yield return StartCoroutine(LerpColour(Source, Dest));
        yield return StartCoroutine(LerpColour(Dest, Source));
    }

    /// <summary>
    /// Method to lerp over time, from Colour x to colour y.
    /// </summary>
    /// <param name="x">Colour to change from.</param>
    /// <param name="y">Colour to change to.</param>
    /// <returns>IEnumerator controlling the damage feedback animation.</returns>
    private IEnumerator LerpColour(Color x, Color y)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime <= TransitionTime)
        {
            //Update elapsed time
            elapsedTime += Time.deltaTime;

            foreach (SpriteRenderer sr in spriteRenderers)
            {
                sr.color = Color.Lerp(x, y, Mathf.Clamp(elapsedTime / TransitionTime, 0.0f, 1.0f));
            }

            //Wait until next frame
            yield return null;
        }

        //Set destination colour
        foreach (SpriteRenderer sr in spriteRenderers)
        {
            sr.color = y;
        }
    }

    #endregion Coroutines
}
