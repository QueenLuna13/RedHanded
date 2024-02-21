using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour
{
    public Transform player;
    private UnityEngine.AI.NavMeshAgent agent;
    public float wanderRadius = 10f;
    public float wanderTimer = 5f;
    private float timer;
    public float touchingDistance = 1.5f; // Adjust the distance for considering the player touched

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        timer = wanderTimer;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= 5f) // If player is close, follow the player
        {
            agent.destination = player.position;

            // Check if the player is touched
            if (distanceToPlayer <= touchingDistance)
            {
                EndGame();
            }
        }
        else // Wander when player is not close
        {
            Wander();
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

    void Wander()
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
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

    void EndGame()
    {
        // Ends the game
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameOver");
    }

    Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;

        randomDirection += origin;

        UnityEngine.AI.NavMeshHit navHit;

        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
