using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Add this line to use SceneManager

public class Agent_AI : MonoBehaviour
{
    public List<Transform> wayPoint;
    public float lineOfSightDistance = 10f;
    public float lineOfSightAngle = 45f;
    public float patrolDelay = 2f;

    NavMeshAgent navMeshAgent;

    public int currentWaypointIndex = 0;
    private bool isPatrolling = true;
    private float patrolTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
        }
        else if (isPatrolling)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWaypoint = Vector3.Distance(wayPoint[currentWaypointIndex].position, transform.position);

        // Check if the agent is close enough to the current waypoint
        if (distanceToWaypoint <= 2)
        {
            if (patrolTimer <= 0)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
                patrolTimer = patrolDelay;
                // Set the destination to the current waypoint
                navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
            }
            else
            {
                patrolTimer -= Time.deltaTime;
            }
        }
        else
        {
            // Set the destination to the current waypoint
            navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
        }
    }

    private void FollowPlayer()
    {
        // Set the destination to the player's position
        navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        isPatrolling = false; // Stop patrolling when following the player
    }

    private bool CanSeePlayer()
    {
        Vector3 directionToPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);

        if (angleToPlayer <= lineOfSightAngle && directionToPlayer.magnitude <= lineOfSightDistance)
        {
            // Player is within line of sight
            // Handle game over when the enemy touches the player
            Collider playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
            if (playerCollider != null && playerCollider.bounds.Intersects(GetComponent<Collider>().bounds))
            {
                // Game over scenario
                SceneManager.LoadScene("GameOver");
            }

            return true;
        }

        // Player is not within line of sight
        isPatrolling = true; // Resume patrolling when player is out of sight
        return false;
    }
}
