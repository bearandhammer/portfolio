using UnityEngine;
using System.Collections;

[ExecuteInEditMode] //Make the script execute in edit mode (i.e. even when the editor is not in play mode)
public class GUICam : MonoBehaviour 
{
    #region Private Data Fields

    //Camera component
    private Camera cam = null;

    //Cached tranform for camera
    private Transform thisTransform = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables
    /// <summary>
    /// The number of pixels to a single Unity measurement
    /// in world space.
    /// </summary>
    public float PixelToWorldScale = 200.0f;

    #endregion Unity Inspector Public Variables

    #region Start/Update

    /// <summary>
    /// Initialisation logic for the
    /// GUICam class.
    /// </summary>
    void Start()
    {
        //Debug.Log("In GUICam Start()");

        //Get camera component for GUI
        cam = GetComponent<Camera>();

        //Get this objects tranform (camera transform also) - Cache
        thisTransform = transform;
    }

    /// <summary>
    /// Update logic for GUICam. Execute
    /// once per frame.
    /// </summary>
    void Update()
    {
        //Update camera size
        cam.orthographicSize = (Screen.height / 2 / PixelToWorldScale);     //Same formula here as always

        //Offset camera so top-left of screen is position (0, 0) for game objects
        thisTransform.localPosition = new Vector3((Screen.width / 2 / PixelToWorldScale), -(Screen.height / 2 / PixelToWorldScale), thisTransform.localPosition.z);
    } 
    
    #endregion Start/Update
}