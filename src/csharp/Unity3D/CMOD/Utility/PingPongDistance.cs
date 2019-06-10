using UnityEngine;
using System.Collections;

/// <summary>
/// 
/// </summary>
public class PingPongDistance : MonoBehaviour
{
    #region Private Data Fields

    //
    private Transform thisTransform = null;

    #endregion Private Data Fields

    #region Unity Inspector Public Variables

    /// <summary>
    /// 
    /// </summary>
    public Vector3 MoveDir = Vector3.zero;
    
    /// <summary>
    /// 
    /// </summary>
    public float Speed = 0.0f;

    public float TravelDistance = 0.0f;

    #endregion Unity Inspector Public Variables

    #region Coroutines

    /// <summary>
    /// 
    /// </summary>
	IEnumerator Start() 
    {
        //Debug.Log("In PingPongDistance Start()");

        thisTransform = transform;

        //Loop forever
        while (true)
        {
            MoveDir = (MoveDir * -1);

            //Start movement
            yield return StartCoroutine(Travel());
        }
	}

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    IEnumerator Travel()
    {
        float distanceTravelled = 0f;

        //Move
        while (distanceTravelled < TravelDistance)
        {
            Vector3 distToTravel = (MoveDir * Speed * Time.deltaTime);
            thisTransform.position += distToTravel;

            distanceTravelled += distToTravel.magnitude;

            yield return null;
        }
    }

    #endregion Coroutines
}
