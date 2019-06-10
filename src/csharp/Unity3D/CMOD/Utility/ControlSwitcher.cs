using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class ControlSwitcher : MonoBehaviour
{
    #region Unity Inspector Public Variables

    /// <summary>
    /// 
    /// </summary>
    public GameObject DesktopFirstPersonController = null;

    /// <summary>
    /// 
    /// </summary>
    public GameObject MobileFirstPersonController = null;

    #endregion Unity Inspector Public Variables

    #region Awake

    ///<summary>
    /// ControlSwitcher Awake method where we determine the target
    /// platform the game is running on and use the appropriate, stock controller (desktop by default, only
    /// switch to mobile if iPhone, Andriod or Windows Mobiles are detected).
    /// </summary>
    void Awake()
    {
#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8

        DesktopFirstPersonController.SetActive(false);
        MobileFirstPersonController.SetActive(true);

#endif
    }

    #endregion Awake
}
