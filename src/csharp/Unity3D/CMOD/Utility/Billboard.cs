using UnityEngine;
using System.Collections;

/// <summary>
/// Billboard utility script (to align a 2D sprite 
/// with the main game camera).
/// </summary>
public class Billboard : MonoBehaviour
{
    #region Private Data Fields

    //The transform relating to this billboard object (for caching)
    private Transform thisTransform = null;

    //The transform relating to the main game camera (for caching)
    private Transform cameraTransform = null;

    #endregion Private Data Fields

    #region Public Methods

    /// <summary>
    /// Start method (for initialisation logic).
    /// </summary>
    void Start()
    {
        //Debug.Log("In Billboard Start()");

        //Set cache transform variables for further use
        thisTransform = transform;
        cameraTransform = Camera.main.transform;
    }

    /// <summary>
    /// LateUpdate method (after Update has been called).
    /// </summary>
    void LateUpdate()
    {
        //As we are sure the camera has finished moving for the given frame calculate where the billboard would need to face to be aligned at the camera (and change the rotation)
        Vector3 LookAtDir = new Vector3(cameraTransform.position.x - thisTransform.position.x, 0, cameraTransform.position.z - thisTransform.position.z);
        thisTransform.rotation = Quaternion.LookRotation(-LookAtDir.normalized, Vector3.up);
    }

    #endregion Public Methods
}