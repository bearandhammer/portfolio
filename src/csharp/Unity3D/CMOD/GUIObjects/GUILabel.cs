using UnityEngine;
using System.Collections;

/// <summary>
/// A simple GUILabel class (to be used to render
/// exceptionally simple content to the screen only).
/// </summary>
[ExecuteInEditMode]                     //View in edit mode
public class GUILabel : MonoBehaviour
{
    #region Unity Inspector Public Variables

    /// <summary>
    /// Content for the label
    /// </summary>
    public GUIContent LabelData;

    /// <summary>
    /// Style for the label.
    /// </summary>
    public GUIStyle LabelStyle;

    /// <summary>
    /// Rect for the label.
    /// </summary>
    public Rect LabelRegion;

    #endregion Unity Inspector Public Variables

    #region OnGUI

    /// <summary>
    /// Draw the label in the appropriate screen location.
    /// </summary>
    void OnGUI()
    {
        Rect finalRect = new Rect
            (
                LabelRegion.x * Screen.width, 
                LabelRegion.y * Screen.height,
                LabelRegion.width * Screen.width,
                LabelRegion.height * Screen.height
            );

        GUI.Label(finalRect, LabelData, LabelStyle);
    }

    #endregion OnGUI
}
