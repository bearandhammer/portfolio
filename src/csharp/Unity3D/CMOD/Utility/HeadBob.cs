using UnityEngine;
using System.Collections;

/// <summary>
/// Utility class that makes a camera 'bob' up and down
/// to mimic head movement (for an FPS game).
/// </summary>
public class HeadBob : MonoBehaviour 
{
    //
    private Transform thisTransform = null;

    private float elapsedTime = 0.0f;

    /// <summary>
    /// 
    /// </summary>
    public float Strength = 1.0f;

    /// <summary>
    /// 
    /// </summary>
    public float BobAmount = 2.0f;

    /// <summary>
    /// 
    /// </summary>
    public float HeadY = 1.0f;

	/// <summary>
	/// 
	/// </summary>
    void Start () 
    {
        //Debug.Log("In HeadBob Start()");
        thisTransform = transform;
	}
	
	/// <summary>
	/// 
	/// </summary>
    void Update () 
    {
        //Input dissallowed at this time, simply exit
        if (!GameManager.Instance.InputAllowed)
        {
            return;
        }

        //Consider a refactor to shorten this - Using original book code so I can remember how this works for now
        float horizontal = Mathf.Abs(Input.GetAxis("Horizontal")), vertical = Mathf.Abs(Input.GetAxis("Vertical")),
            totalMovement = Mathf.Clamp((horizontal + vertical), 0.0f, 1.0f);

        elapsedTime = (totalMovement > 0.0f) ? elapsedTime += Time.deltaTime : 0.0f;

        //Y Offset for headbob
        float yOffset = Mathf.Sin(elapsedTime * BobAmount) * Strength;

        //Create position
        Vector3 playerPos = new Vector3(thisTransform.position.x, ((yOffset * totalMovement) + HeadY), thisTransform.position.z);

        //Update position
        this.transform.position = playerPos;
	}
}
