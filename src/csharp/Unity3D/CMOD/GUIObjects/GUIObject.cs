using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Base GUIObject class.
/// </summary>
[ExecuteInEditMode]     //Available in edit mode
public class GUIObject : MonoBehaviour 
{
    #region Private Data Fields

    //This objects transform (cached)
    private Transform thisTransform = null;

    #endregion Private Data Fields

    #region Pixel Padding Class

    /// <summary>
    /// Pixel padding class definition that keeps track
    /// of left, right, top and bottom padding (on the screen).
    /// </summary>
    [Serializable]
    public class PixelPadding
    {
        /// <summary>
        /// Normalised left padding (0 - 1).
        /// </summary>
        public float LeftPadding;

        /// <summary>
        /// Normalised right padding (0 - 1).
        /// </summary>
        public float RightPadding;

        /// <summary>
        /// Normalised top padding (0 - 1).
        /// </summary>
        public float TopPadding;

        /// <summary>
        /// Normalised bottom padding (0 - 1).
        /// </summary>
        public float BottomPadding;
    } 
    
    #endregion Pixel Padding Class

    #region Public Enums
    
    /// <summary>
    /// Horizontal alignment enum.
    /// </summary>
    public enum HorizAlign
    {
        Left = 0,
        Right = 1
    }

    /// <summary>
    /// Vertical alignment enum.
    /// </summary>
    public enum VertAlign
    {
        Top = 0,
        Bottom = 1
    } 

    #endregion Public Enums

    #region Unity Inspector Public Variables

    //NOTE TO SELF - PROBABLY BE BETTER AS A STRUCTURE RIGHT??? Keeping with the book code for now

    /// <summary>
    /// Pixel padding instance (to record screen buffers).
    /// </summary>
    public PixelPadding Padding;

    /// <summary>
    /// The horizontal alignment type for this object.
    /// </summary>
    public HorizAlign horAlign = HorizAlign.Left;

    /// <summary>
    /// The vertical alignment type for this object.
    /// </summary>
    public VertAlign verAlign = VertAlign.Top;

    /// <summary>
    /// A reference the GUICamera for this object.
    /// </summary>
    public GUICam GUICamera = null; 

    #endregion Unity Inspector Public Variables

    #region Start/Update

    /// <summary>
    /// Initialisation logic for 
    /// the GUIObject class.
    /// </summary>
    void Start()
    {
        //Debug.Log("In GUIObject Start()");

        //Cache this objects transform
        thisTransform = transform;
    }

    /// <summary>
    /// Update method for the GUIObject
    /// (called once per frame).
    /// </summary>
    void Update()
    {
        //If the window is resized or the resolution is changed we want our GUI elements to remain positioned properly

        //Calculate this objects required position on screen (based on the values specified in the inspector by the developer)
        Vector3 finalPosition = new Vector3
            (
                horAlign == HorizAlign.Left ? 0.0f : Screen.width,      //horizontal alignment is 0.0f (far left) else the screen.width (far right of the screen)
                verAlign == VertAlign.Top ? 0.0f : Screen.height,       //vertical aligment is 0.0f (top of screen) else the screen.height (far bottom of the screen)
                thisTransform.position.z                                //Not taking this into account, just use the same position
            );

        //Offset with padding
        finalPosition = new Vector3
            (
                (finalPosition.x + ((Padding.LeftPadding * Screen.width) - (Padding.RightPadding * Screen.width))),     //Position element on the x axis based on alignment and padding
                (finalPosition.y - ((Padding.TopPadding  * Screen.height) + (Padding.BottomPadding * Screen.height))),  //Position element on the y axis based on alignment and padding
                finalPosition.z                                                                                         //Again, this isn't
            );

        //Convert to pixel scale (using the cameras pixel to world scale)
        finalPosition = new Vector3
            (
                finalPosition.x / GUICamera.PixelToWorldScale,
                finalPosition.y / GUICamera.PixelToWorldScale,
                finalPosition.z
            );

        //Update position
        thisTransform.localPosition = finalPosition; //Position in relation to the parent orthagraphic camera, not world position!
    } 

    #endregion Start/Update
}