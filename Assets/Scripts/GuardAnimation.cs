using UnityEngine;

public class GuardAnimation : MonoBehaviour
{
    public Animator animator;
    private bool isWalking = false;
    private float timeBetweenWalks = 5f; // Adjust as needed
    private float timer = 0f;

    void Start()
    {
        // Get the Animator component from the GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to toggle between walking and standing
        if (timer >= timeBetweenWalks)
        {
            // Toggle isWalking
            isWalking = !isWalking;

            // Reset the timer
            timer = 0f;

            // Gradually transition to the walk animation
            if (isWalking)
            {
                animator.CrossFade("WalkAnimation", 0.25f); // Adjust the blending time as needed
            }
            else
            {
                animator.CrossFade("IdleAnimation", 0.25f); // Adjust the blending time as needed
            }

            // Debug log to check if animation is triggered
            Debug.Log("Walking animation triggered. isWalking: " + isWalking);
        }
    }
}