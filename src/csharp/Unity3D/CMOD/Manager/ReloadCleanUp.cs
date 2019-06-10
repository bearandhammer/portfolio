using UnityEngine;
using System.Collections;

/// <summary>
/// Clean up class that runs as the last script on 
/// startup (to clear up the Notifications class).
/// </summary>
public class ReloadCleanUp : MonoBehaviour
{
    #region Start

    /// <summary>
    /// Runs first when a scene is loaded or restarted.
    /// </summary>
    void Start()
    {
        GameManager.Notifications.RemoveRedundancies();
    }

    #endregion Start
}