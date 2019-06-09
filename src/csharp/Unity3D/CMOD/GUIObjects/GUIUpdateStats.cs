using UnityEngine;
using System.Collections;

/// <summary>
/// GUIUpdateStats class that updates
/// GUILabels for this game.
/// </summary>
public class GUIUpdateStats : MonoBehaviour
{
    #region Private Data Fields

    //A reference to the player object to retrieve health/ammo amounts
    private PlayerController player = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// The Health GUILabel.
    /// </summary>
    public GUILabel HealthLabel = null;

    /// <summary>
    /// The Ammo GUILabel.
    /// </summary>
    public GUILabel AmmoLabel = null;
    
    #endregion Unity Inspector Public Variables

    #region Start/Update

    /// <summary>
    /// Initialition method.
    /// </summary>
    void Start()
    {
        //Debug.Log("In GUIUpdateStats Start()");

        //Retrieve a reference to the player controller
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Update method for GUIUpdateStates that 
    /// runs once per frame.
    /// </summary>
    void Update()
    {
        //Update health and ammo labels based on the player controller values
        HealthLabel.LabelData.text = string.Concat("Health: ", Mathf.Clamp(player.Health, 0, 100));
        AmmoLabel.LabelData.text = string.Concat("Ammo: ", player.ActiveWeapon.Ammo <= 0 ? "None" : player.ActiveWeapon.Ammo.ToString());
    }

    #endregion Start/Update
}