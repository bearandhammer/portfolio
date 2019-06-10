using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents our core Event Handling Class for the 
/// CMOD FPS game.
/// </summary>
public class NotificationsManager : MonoBehaviour
{
    #region Private Data Fields

    //Private dictionary that tracks listeners for given event types
    private Dictionary<string, List<Component>> listeners = new Dictionary<string, List<Component>>();

    #endregion Private Data Fields

    #region Public Methods

    /// <summary>
    /// Ties the provided listener object (component) to the specified
    /// event type. If the event type is not yet being handled then a new dictionary 
    /// entry is created for the event type (notification name) and a new list of components
    /// instantiated ready for additions as required.
    /// </summary>
    /// <param name="sender">The component to be notified of a given event.</param>
    /// <param name="notificationName">The event to tie to the provided listener object.</param>
    public void AddListener(Component sender, string notificationName)
    {
        //Check to see if this notification (event) type is currently stored locally. If not, create a new dictionary entry for it
        if (!listeners.ContainsKey(notificationName))
        {
            listeners.Add(notificationName, new List<Component>());
        }

        //Tie a listener object to the given notification (event) type
        listeners[notificationName].Add(sender);
    }

    /// <summary>
    /// Allow specific listeners to unregistered themselves for a given
    /// event/notification type.
    /// </summary>
    /// <param name="sender">The object that no longer needs to listen for the given event.</param>
    /// <param name="notificationName">The event/notification type to be removed from.</param>
    public void RemoveListener(Component sender, string notificationName)
    {
        //Debug.Log("Removing listeners");

        //See if the notification type is supported currently. If not, then return
        if (!listeners.ContainsKey(notificationName))
        {
            return;
        }

        //Remove 'all' references that match (by instance id) for the given notification type
        listeners[notificationName].RemoveAll(li => li.GetInstanceID() == sender.GetInstanceID());
    }

    /// <summary>
    /// Allow for an event 'poster' to trigger a named method (based on the notification
    /// name) on all listening objects.
    /// </summary>
    /// <param name="sender">The poster who has latched onto an event in the first instance.</param>
    /// <param name="notificationName">The event/notification name (ties to a method name on listening objects).</param>
    public void PostNotification(Component sender, string notificationName)
    {
        //If there are no references based on the notification name then simply return (no work to do)
        if (!listeners.ContainsKey(notificationName))
        {
            return;
        }

        //Notify each, relevant, object of that a specific event has occurred
        listeners[notificationName].ForEach(li =>
        {
            if (li != null)
            {
                li.SendMessage(notificationName, sender, SendMessageOptions.DontRequireReceiver);
            }
        });
    }

    /// <summary>
    /// Removes redundant listeners (to cover scenarios where objects might be removed
    /// from the scene without detaching themselves from events).
    /// </summary>
    public void RemoveRedundancies()
    {
        //Create a new dictionary (ready for an optimised list of notifications/listeners)
        Dictionary<string, List<Component>> tempListeners = new Dictionary<string, List<Component>>();
        
        //Iterate through the notification/listener list and removing null listener objects. only keep a notification/listener dictionary entry if one or more
        //listening objects still exist for a given notification
        listeners.ToList().ForEach(li =>
        {
            li.Value.RemoveAll(listObj => listObj == null);

            if (li.Value.Count > 0)
            {
                tempListeners.Add(li.Key, li.Value);
            }
        });

        //Set the listener dictionary based on the optimised/temporary dictionary
        listeners = tempListeners;
    }
    
    /// <summary>
    /// Removes all listener dictionary references.
    /// </summary>
    public void ClearListeners()
    {
        listeners.Clear();
    }
    
    #endregion Public Methods
}