using UnityEngine;
using System.Collections;

public class PowerupAmmo : MonoBehaviour 
{
    private AudioSource audSource = null;
    
    public int Ammo = 10;

    public Weapon AmmoWeapon = null;

    public AudioClip Clip = null;

    void Start()
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

        if (AmmoWeapon.Collected)
        {
            AmmoWeapon.Ammo += Ammo;
        }

        GameManager.Notifications.PostNotification(this, "PowerupCollected");
    }
}
