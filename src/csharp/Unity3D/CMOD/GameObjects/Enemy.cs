using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Abstract base class that outlines the 
/// concept of an enemy.
/// </summary>
public abstract class Enemy : MonoBehaviour
{
    #region Public Enums

    /// <summary>
    /// Enum state for the enemy FSM.
    /// </summary>
    public enum EnemyState
    {
        Patrol = 0,
        Chase = 1,
        Attack = 2
    }

    /// <summary>
    /// Enum for enemy types.
    /// </summary>
    public enum EnemyType
    {
        Drone = 0,
        ToughGuy = 1,
        Boss = 2
    }

    #endregion Public Enums

    #region Unity Inspector Public Variables

    /// <summary>
    /// The enemies type.
    /// </summary>
    public EnemyType Type = EnemyType.Drone;

    /// <summary>
    /// Active enemy state (defaulted to patrol).
    /// </summary>
    public EnemyState ActiveState = EnemyState.Patrol;

    /// <summary>
    /// The custom ID of this enemy.
    /// </summary>
    public int EnemyID = 0;

    /// <summary>
    /// Represents the enemies current health.
    /// </summary>
    public int Health = 100;

    /// <summary>
    /// Attack Damage - amount of damage the enemy
    /// deals to the player.
    /// </summary>
    public int AttackDamage = 10;

    /// <summary>
    /// Recovery delay in seconds after launching an attack.
    /// </summary>
    public float RecoveryDelay = 1.0f;

    /// <summary>
    /// Total distance in Unity Units from current position 
    /// that agent can wander when patrolling.
    /// </summary>
    public float PatrolDistance = 10.0f;

    /// <summary>
    /// Total distance the enemy must be from player, in Unity units, before
    /// chasing them (entering chase state).
    /// </summary>
    public float ChaseDistance = 10.0f;

    /// <summary>
    /// Total distance enemy must be from the player before
    /// attacking them.
    /// </summary>
    public float AttackDistance = 0.1f;

    #endregion Unity Inspector Public Variables

    #region Protected Fields

    //Reference to the active PlayerController component for the player
    protected PlayerController playerCont = null;

    //Enemy cached transform
    protected Transform thisTranform = null;

    //Reference to the player transform
    protected Transform playerTransform = null;

    //The Nav Mesh attached to this enemy (for pathfinding)
    protected NavMeshAgent agent = null;

    #endregion Protected Fields

    #region Start

    /// <summary>
    /// Initialisation logic for an enemy.
    /// </summary>
    protected virtual void Start()
    {
        //Retrieve the Nav Mesh Agent for this enemy (cache it)
        agent = GetComponent<NavMeshAgent>();

        //How about? Get reference to player controller by using the controller 'tag'
        playerCont = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        //Get player transform
        playerTransform = playerCont.transform;

        //This enemies transform
        thisTranform = transform;

        //Set default state
        ChangeState(ActiveState);

        GameManager.Notifications.AddListener(this, "SaveGamePrepare");
        GameManager.Notifications.AddListener(this, "LoadGameComplete");
    }

    #endregion Start

    #region Public Methods

    /// <summary>
    /// Change AI State.
    /// </summary>
    /// <param name="ActiveState"></param>
    public void ChangeState(EnemyState state)
    {
        //Maybe consider checking the state? Has it changed?

        //First, stop all currently running AI processing
        StopAllCoroutines();

        //Set new state and activate it
        ActiveState = state;

        //Start coroutines and in each case notify the game object in case we want to handle state change (might not just be here, perhaps in other components)
        switch (ActiveState)
        {
            case EnemyState.Attack:
                {
                    StartCoroutine(AiAttack());
                    SendMessage("Attack", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            case EnemyState.Chase:
                {
                    StartCoroutine(AiChase());
                    SendMessage("Chase", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            case EnemyState.Patrol:
                {
                    StartCoroutine(AiPatrol());
                    SendMessage("Patrol", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            default:
                {
                    return; //Nothing else to do, return
                }
        }
    }

    /// <summary>
    /// Prepare data to save this enemy.
    /// </summary>
    /// <param name="sender">The component sender object.</param>
    public void SaveGamePrepare(Component sender)
    {
        //Create a reference for this enemy
        LoadSaveManager.GameStateData.DataEnemy thisEnemy = new LoadSaveManager.GameStateData.DataEnemy();
        
        //Fill in data for the current enemy
        thisEnemy.EnemyID = EnemyID;
        thisEnemy.Health = Health;
        thisEnemy.PosRotScale.X = thisTranform.position.x;
        thisEnemy.PosRotScale.Y = thisTranform.position.y;
        thisEnemy.PosRotScale.Z = thisTranform.position.z;
        thisEnemy.PosRotScale.RotX = thisTranform.localEulerAngles.x;
        thisEnemy.PosRotScale.RotY = thisTranform.localEulerAngles.y;
        thisEnemy.PosRotScale.RotZ = thisTranform.localEulerAngles.z;
        thisEnemy.PosRotScale.ScaleX = thisTranform.localScale.x;
        thisEnemy.PosRotScale.ScaleY = thisTranform.localScale.y;
        thisEnemy.PosRotScale.ScaleZ = thisTranform.localScale.z;
        
        //Add this enemy to the list
        GameManager.StateManager.GameState.Enemies.Add(thisEnemy);
    }

    /// <summary>
    /// Prepare data to load this enemy.
    /// </summary>
    /// <param name="sender">The component sender object.</param>
    public void LoadGameComplete(Component sender)
    {
        //Cycle through enemies and find matching ID
        List<LoadSaveManager.GameStateData.DataEnemy> enemies = GameManager.StateManager.GameState.Enemies;

        //Reference to this enemy
        LoadSaveManager.GameStateData.DataEnemy thisEnemy = null;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].EnemyID == EnemyID)
            {
                //Found enemy. Now break break from loop
                thisEnemy = enemies[i];
                break;
            }
        }

        //If we can't find this enemy then it must have been destroyed on save
        if (thisEnemy == null)
        {
            DestroyImmediate(gameObject);
            return;
        }
          
        //We've got this far so load the enemy data
        EnemyID = thisEnemy.EnemyID;    //This is set from the inspector so not much point in reloading, keeping with the book code however
        Health = thisEnemy.Health;
            
        //Set position, rotation and scale (position done with warp)
        agent.Warp(new Vector3(thisEnemy.PosRotScale.X, thisEnemy.PosRotScale.Y, thisEnemy.PosRotScale.Z));
        thisTranform.localRotation = Quaternion.Euler(thisEnemy.PosRotScale.RotX, thisEnemy.PosRotScale.RotY, thisEnemy.PosRotScale.RotZ);
        thisTranform.localScale = new Vector3(thisEnemy.PosRotScale.ScaleX, thisEnemy.PosRotScale.ScaleY, thisEnemy.PosRotScale.ScaleZ);
    }

    #endregion Public Methods

    #region Coroutines

    /// <summary>
    /// AI method that handles attack behaviour for the enemy.
    /// Can exit this state and enter either patrol or chase.
    /// </summary>
    /// <returns>IEnumerator.</returns>
    public virtual IEnumerator AiAttack()
    {
        //Stop agent, ready for a new instruction
        agent.Stop();

        //Elapsed time, to calculate strike intervals (set to recovery delay so an attack is possible immediately after the enemy closes distance)
        float elapsedTime = RecoveryDelay;

        //Loop forever while in the attack state
        while (ActiveState == EnemyState.Attack)
        {
            //Update elapsed time
            elapsedTime += Time.deltaTime;

            //Check distances and state exit conditions
            float distanceFromPlayer = Vector3.Distance(thisTranform.position, playerTransform.position);

            //If outside of chase range, then revert to patrol
            if (distanceFromPlayer > ChaseDistance)
            {
                ChangeState(EnemyState.Patrol);
                yield break;
            }

            //If outside of attack range, then change to chase
            if (distanceFromPlayer > AttackDistance)
            {
                ChangeState(EnemyState.Chase);
                yield break;
            }

            //Make strike
            if (elapsedTime >= RecoveryDelay)
            {
                elapsedTime = 0;
                SendMessage("Strike", SendMessageOptions.DontRequireReceiver);
            }

            //Wait until the next frame
            yield return null;
        }
    }

    /// <summary>
    /// AI method that handles attack behaviour for the enemy.
    /// Can exit this state and enter either attack or patrol.
    /// </summary>
    /// <returns>IEumerator.</returns>
    public virtual IEnumerator AiChase()
    {
        //Stop agent, ready for a new instruction
        agent.Stop();

        //Whilst we are in the chase state then loop forever
        while (ActiveState == EnemyState.Chase)
        {
            //Set destination to the player
            agent.SetDestination(playerTransform.position);

            //Check the distance between the enemy and the player to look for state changes
            float distanceFromPlayer = Vector3.Distance(thisTranform.position, playerTransform.position);

            //If within attack range then alter to that state
            if (distanceFromPlayer < AttackDistance)
            {
                ChangeState(EnemyState.Attack);
                yield break;
            }

            //Enemy is out of range to chase, revert to the patrol state
            if (distanceFromPlayer > ChaseDistance)
            {
                ChangeState(EnemyState.Patrol);
                yield break;
            }

            //Wait until the next frame
            yield return null;
        }
    }

    /// <summary>
    /// AI method that handles attack behaviour for the enemy.
    /// Can only enter the chase state from here (once the distance
    /// to the player closes sufficiently).
    /// </summary>
    /// <returns>IEnumerator.</returns>
    public virtual IEnumerator AiPatrol()
    {
        //Stop agent, ready for a new destination
        agent.Stop();

        //Loop forever whilst in the patrol state
        while (ActiveState == EnemyState.Patrol)
        {
            //Get random location destination on the map (somewhere within a sphere with a radius based on PatrolDistance (center at zero))
            Vector3 randomPosition = (Random.insideUnitSphere * PatrolDistance);

            //Add as offset from current position
            randomPosition += thisTranform.position;

            //Get nearest valid position (on the Nav Mesh)
            NavMeshHit hit;
            NavMesh.SamplePosition(randomPosition, out hit, PatrolDistance, 1);

            //Set destination for this enemy
            agent.SetDestination(hit.position);

            /*
             * Set distance range between object and destination to classify as 'arrived' +
             * Set timeout before new path is generated (as a fail safe, destination too far or enemy could be having difficulty reaching the location) +
             * Create a elapsed time float to measure against the timeout
            */
            float arrivalDistance = 2.0f, timeOut = 5.0f, elapsedTime = 0;

            //Wait until the enemy reaches the destination or times-out, then get the new position
            while (Vector3.Distance(thisTranform.position, hit.position) > arrivalDistance && elapsedTime < timeOut)
            {
                //Update elapsed time
                elapsedTime += Time.deltaTime;

                //Can only enter chase (once the distance closes sufficiently). Can then move to other FSM states from there
                if (Vector3.Distance(thisTranform.position, playerTransform.position) < ChaseDistance)
                {
                    ChangeState(EnemyState.Chase);
                    yield break;
                }

                yield return null;
            }
        }
    }

    #endregion Coroutines
}