using UnityEngine;
using System.Collections;

public class PowerupBurger : MonoBehaviour 
{
    private AudioSource audSource = null;

    public int HealthAmount = 10;

    public AudioClip Clip = null;

    /// <summary>
    /// 
    /// </summary>
	void Start () 
    {
        GameObject soundsObject = GameObject.FindGameObjectWithTag("Sounds");

        if (soundsObject == null)
        {
            return;
        }

        audSource = soundsObject.GetComponent<AudioSource>();	
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
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
            playerCont.Health = playerCont.Health >= 91 ? 100 : playerCont.Health += HealthAmount;
        }

        GameManager.Notifications.PostNotification(this, "PowerupCollected");
    }
}
