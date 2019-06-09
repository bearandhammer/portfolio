using UnityEngine;
using System.Collections;

/// <summary>
/// Abstract weapon base class (contains core members
/// for all weapons in the CMOD game - Used as a base).
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    #region Public Enums 
    
    /// <summary>
    /// Public enum denoting weapon types available in the game
    /// </summary>
    public enum WeaponType
    {
        Punch,
        Gun
    }

    #endregion Public Enums

    #region Unity Inspector Public Variables

    /// <summary>
    /// The weapon type of an inheriting class.
    /// </summary>
    public WeaponType Type = WeaponType.Punch;

    /// <summary>
    /// The damage this weapon causes.
    /// </summary>
    public float Damage = 0.0f;

    /// <summary>
    /// The range of this weapon 
    /// (linear distance outwards from the camera).
    /// </summary>
    public float Range = 1.0f;

    /// <summary>
    /// Amount of ammo remaining (-1 = infinite).
    /// </summary>
    public int Ammo = -1;

    /// <summary>
    /// Recovery delay. 
    /// Time in seconds before the weapon can be reused.
    /// </summary>
    public float RecoveryDelay = 0.0f;

    /// <summary>
    /// Has this weapon been collected.
    /// </summary>
    public bool Collected = false;

    /// <summary>
    /// Is this weapon currently equipped on the player.
    /// </summary>
    public bool IsEquipped = false;

    /// <summary>
    /// Can this weapon be fired.
    /// </summary>
    public bool CanFire = true;

    /// <summary>
    /// Next weapon in cycle.
    /// </summary>
    public Weapon NextWeapon = null;

    #endregion Unity Inspector Public Variables
}
