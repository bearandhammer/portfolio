using UnityEngine;
using System.Collections;

public class PowerupDollar : MonoBehaviour
{
    #region Private Data Fields

    //
    private AudioSource audSource = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// 
    /// </summary>
    public float CashAmount = 100.0f;
    
    /// <summary>
    /// 
    /// </summary>
    public AudioClip Clip = null;

    #endregion Unity Inspector Public Variables

    #region Start Method

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        //Find the sound object in the scene
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        if (soundsObject == null)
        {
            return;
        }

        audSource = soundsObject.GetComponent<AudioSource>();
    }

    #endregion Start Method

    #region Trigger

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        if (audSource != null)
        {
            audSource.PlayOneShot(Clip, 1.0f);
        }

        gameObject.SetActive(false);

        PlayerController playerCont = other.gameObject.GetComponent<PlayerController>();

        if (playerCont != null)
        {
            playerCont.Cash += CashAmount;
        }

        GameManager.Notifications.PostNotification(this, "PowerupCollected");
    }

    #endregion Trigger
}