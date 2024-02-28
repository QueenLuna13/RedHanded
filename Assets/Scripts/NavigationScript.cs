using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingAndChasingScript : MonoBehaviour
{
    public List<Transform> waypoints; // Define waypoints in the Inspector
    public Transform player; // Reference to the player object
    private UnityEngine.AI.NavMeshAgent agent;
    public float patrolStoppingDistance = 0.1f; // Adjust the stopping distance for patrolling
    public float waitTimeAtWaypoint = 2f; // Adjust the time to stop at each waypoint
    public float fieldOfViewAngle = 90f; // Adjust the field of view angle
    public float maxViewDistance = 10f; // Maximum viewing distance for the guard
    public float chasingStoppingDistance = 1.5f; // Adjust the stopping distance for chasing the player

    private int currentWaypointIndex = 0;
    private bool isWaitingAtWaypoint = false;
    private float waitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // Check if there are waypoints defined
        if (waypoints.Count == 0)
        {
            Debug.LogError("No waypoints defined for patrolling!");
            enabled = false; // Disable the script if no waypoints are defined
            return;
        }

        SetDestinationToNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayerInFieldOfView())
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Check if the enemy has reached the current waypoint
        if (agent.remainingDistance < patrolStoppingDistance && !agent.pathPending)
        {
            if (!isWaitingAtWaypoint)
            {
                StartWaitingAtWaypoint();
            }
            else
            {
                ContinueWaiting();
            }
        }
    }

    void StartWaitingAtWaypoint()
    {
        // Stop at the current waypoint
        agent.isStopped = true;
        isWaitingAtWaypoint = true;
        waitTimer = 0f;
    }

    void ContinueWaiting()
    {
        // Increment the wait timer
        waitTimer += Time.deltaTime;

        // Check if the wait time is over
        if (waitTimer >= waitTimeAtWaypoint)
        {
            // Resume patrolling
            SetDestinationToNextWaypoint();
            isWaitingAtWaypoint = false;
            agent.isStopped = false;
        }
    }

    void ChasePlayer()
    {
        // Set the destination to the player's position if the player is within the maximum view distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= maxViewDistance)
        {
            agent.destination = player.position;

            // Check if the player is touched
            if (distanceToPlayer <= chasingStoppingDistance)
            {
                EndGame();
            }
        }
        else
        {
            // Player is out of maximum view distance, resume patrolling
            Patrol();
        }
    }

    bool IsPlayerInFieldOfView()
    {
        if (player == null)
        {
            Debug.LogError("Player reference not set!");
            return false;
        }

        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        return angleToPlayer <= fieldOfViewAngle * 0.5f;
    }

    void SetDestinationToNextWaypoint()
    {
        // Set the destination to the next waypoint in the list
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        agent.destination = waypoints[currentWaypointIndex].position;
    }

    void EndGame()
    {
        // Ends the game
        Debug.Log("Game Over");
        // Add your game over logic or scene transition here
    }
}
