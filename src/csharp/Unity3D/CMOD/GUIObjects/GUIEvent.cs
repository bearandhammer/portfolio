using UnityEngine;
using System.Collections;

/// <summary>
/// GUIEvent class for forwarding notifications
/// (when menu items are clicked).
/// </summary>
public class GUIEvent : MonoBehaviour
{
    #region Unity Inspector Public Variables

    /// <summary>
    /// The notification to post to the GameManager based
    /// on the notification tied to this GUIEvent.
    /// </summary>
    public string Notification = null;

    #endregion Unity Inspector Public Variables

    #region OnMouseDown Event

    /// <summary>
    /// 
    /// </summary>
    void OnMouseDown()
    {
        //If the notification is set then post it to the GameManager
        if (!string.IsNullOrEmpty(Notification))
        {
            //Debug.Log(Notification);
            GameManager.Notifications.PostNotification(this, Notification);
        }
    }

    #endregion OnMouseDown Event
}
