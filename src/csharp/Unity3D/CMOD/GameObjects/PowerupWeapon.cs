using UnityEngine;
using System.Collections;

public class PowerupWeapon : MonoBehaviour
{
    private AudioSource audSource = null;

    public Weapon CollectWeapon = null;

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

        CollectWeapon.Collected = true;

        if (playerCont != null)
        {
            playerCont.EquipNextWeapon();
        }

        GameManager.Notifications.PostNotification(this, "PowerupCollected");
    }
}