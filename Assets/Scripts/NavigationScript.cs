using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    public List<Transform> waypoints; // Define waypoints in the Inspector
    private UnityEngine.AI.NavMeshAgent agent;
    public float patrolStoppingDistance = 0.1f; // Adjust the stopping distance for patrolling
    public float chasingStoppingDistance = 1.5f; // Adjust the stopping distance for chasing the player
    public float touchingDistance = 1.5f; // Adjust the distance for considering the player touched

    private int currentWaypointIndex = 0;
    private int lastPatrolWaypointIndex = 0;
    private bool isChasingPlayer = false;

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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chasingStoppingDistance) // If player is close, chase the player
        {
            // Check if the player is touched
            if (distanceToPlayer <= touchingDistance)
            {
                EndGame();
            }
            else
            {
                // Set the destination to the player
                agent.destination = player.position;
                isChasingPlayer = true;
            }
        }
        else // Patrol between waypoints when the player is not close
        {
            // If returning from chasing, set the destination to the last waypoint reached
            if (isChasingPlayer && agent.remainingDistance < patrolStoppingDistance && !agent.pathPending)
            {
                currentWaypointIndex = lastPatrolWaypointIndex;
                isChasingPlayer = false;
            }

            // Check if the enemy has reached the current waypoint
            if (agent.remainingDistance < patrolStoppingDistance && !agent.pathPending && !isChasingPlayer)
            {
                SetDestinationToNextWaypoint();
            }
        }

        // Check if the player is in line of sight
        if (IsPlayerInLineOfSight())
        {
            // Visualize the line of sight
            Debug.DrawLine(transform.position, player.position, Color.red);
            // End the game
            EndGame();
        }
    }

    bool IsPlayerInLineOfSight()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.position - transform.position, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Player is in line of sight
                return true;
            }
        }

        // Player is not in line of sight
        return false;
    }

    void SetDestinationToNextWaypoint()
    {
        // Set the destination to the next waypoint in the list
        agent.destination = waypoints[currentWaypointIndex].position;

        // Increment the waypoint index for the next frame
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;

        // Save the current waypoint index as the last patrol waypoint index
        lastPatrolWaypointIndex = currentWaypointIndex;
    }

    void EndGame()
    {
        // Ends the game
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }
}
