using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public List<Transform> wayPoint;
    public float lineOfSightDistance = 10f;
    public float lineOfSightAngle = 45f;
    public float patrolDelay = 2f;
    public float touchingDistance = 2f; // Adjust this distance for touching threshold
    public GameObject followOverlay;

    NavMeshAgent navMeshAgent;

    public int currentWaypointIndex = 0;
    private bool isPatrolling = true;
    private float patrolTimer = 0f;

    // FOOTSTEPS AUDIO
    public AudioClip footstepSound;
    public float walkingStepInterval = 0.5f;
    public float sprintingStepInterval = 0.3f;
    public float pitchRange = 0.1f;
    private float stepTimer = 0f;
    private AudioSource audioSource;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        Patrol();
        if (followOverlay != null)
            followOverlay.SetActive(false);
    }

    void Update()
    {
        if (CanSeePlayer())
        {
            FollowPlayer();
            if (followOverlay != null)
                followOverlay.SetActive(true);
        }
        else if (isPatrolling)
        {
            Patrol();
            if (followOverlay != null)
            followOverlay.SetActive(false);
        }

        CheckPlayerGuardCollision();

        // Play footstep sounds based on the enemy's movement
        if (navMeshAgent.velocity.magnitude > 0)
        {
            if (stepTimer <= 0)
            {
                PlayFootstepSound();
                stepTimer = walkingStepInterval; // Assuming enemy only walks
            }
            else
            {
                stepTimer -= Time.deltaTime;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void Patrol()
    {
        if (wayPoint.Count == 0)
        {
            return;
        }

        float distanceToWaypoint = Vector3.Distance(wayPoint[currentWaypointIndex].position, transform.position);

        if (distanceToWaypoint <= 2)
        {
            if (patrolTimer <= 0)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % wayPoint.Count;
                patrolTimer = patrolDelay;
                navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
            }
            else
            {
                patrolTimer -= Time.deltaTime;
            }
        }
        else
        {
            navMeshAgent.SetDestination(wayPoint[currentWaypointIndex].position);
        }
    }

    void FollowPlayer()
    {
        navMeshAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        isPatrolling = false;
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = GameObject.FindGameObjectWithTag("Player").transform.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer.normalized);

        if (angleToPlayer <= lineOfSightAngle && directionToPlayer.magnitude <= lineOfSightDistance)
        {
            isPatrolling = false;
            return true;
        }

        isPatrolling = true;
        return false;
    }

    void CheckPlayerGuardCollision()
    {
        // Check if the guard is close enough to the player to trigger game over
        float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (distanceToPlayer <= touchingDistance)
        {
            // Game over scenario
            SceneManager.LoadScene("GameOver"); // Replace with the name of your game over scene
        }
    }

    void PlayFootstepSound()
    {
        if (footstepSound != null && audioSource != null)
        {
            // Randomize pitch for variation
            float pitch = Random.Range(1f - pitchRange, 1f + pitchRange);
            audioSource.pitch = pitch;

            // Play footstep sound
            audioSource.PlayOneShot(footstepSound);
        }
    }
}
