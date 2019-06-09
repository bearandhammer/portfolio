using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Class for menu functionality.
/// </summary>
public class GUIOptions : MonoBehaviour 
{
    #region Private Data Fields
    
    //Sprite renderer for the menu
    private SpriteRenderer spriteRend = null;

    //Collision objects for buttons
    private List<BoxCollider> collidors = null;

    //The characters x mouse look reference
    //private MouseLook characterMouseLookX = null;

    //The characters y mouse look reference
    //private MouseLook characterMouseLookY = null;

    #endregion Private Data Fields

    #region Start/Update

    /// <summary>
    /// Initialisation logic for the
    /// GUIOptions class.
    /// </summary>
	void Start () 
    {
        //Debug.Log("In GUIoptions Start()");
        //Retrieve the players mouse look object
        //characterMouseLookX = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();     //This is on the 'Player' object
        //characterMouseLookY = Camera.main.GetComponent<MouseLook>();                                    //Mysteriously, this is on the camera, meh

        //Retrieve the menu sprite renderer and all menu item box collidors on start
        spriteRend = GetComponent<SpriteRenderer>();
        collidors = GetComponentsInChildren<BoxCollider>().ToList();

        //Add menu event listeners (show and hide this options menu)
        GameManager.Notifications.AddListener(this, "ShowOptions");
        GameManager.Notifications.AddListener(this, "HideOptions");

        //Hide menu on startup
        HideOptions(null);
	}
	
    /// <summary>
    /// GUIOptions update method that runs
    /// once every frame.
    /// </summary>
	void Update () 
    {
        //If escape is pressed then show the options menu. Otherwise, hide the menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetOptionsVisible(!spriteRend.enabled);
        }
    }

    #endregion Start/Update

    #region Public Methods

    /// <summary>
    /// Public method that shows the options
    /// menu and sets box collidors (menu buttons)
    /// to visible.
    /// </summary>
    /// <param name="sender">The component triggering the event.</param>
    public void ShowOptions(Component sender)
    {
        SetOptionsVisible();
    }

    /// <summary>
    /// Public method that hides the options
    /// menu and sets box collidors (menu buttons)
    /// to not visible.
    /// </summary>
    /// <param name="sender">The component triggering the event.</param>
    public void HideOptions(Component sender)
    {
        SetOptionsVisible(false);
    }

    #endregion Public Methods

    #region Private Methods

    /// <summary>
    /// Function to show and hide the options.
    /// </summary>
    /// <param name="bShow">Parameter to determine the options visibility state.</param>
    private void SetOptionsVisible(bool bShow = true)
    {
        //If enabling, then pause game. Else, resume
        Time.timeScale = bShow ? 0.0f : 1.0f;

        //Enable/disable input and stop camera movement
        GameManager.Instance.InputAllowed = !bShow;

        //Show/hide mouse cursor
        Cursor.visible = bShow;

        //Show/hide menu graphic
        spriteRend.enabled = bShow;

        //Enable/disable button collidors
        collidors.ForEach(bc =>
        {
            bc.enabled = bShow;
        });
    }

    #endregion Private Methods
}