using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float normalSpeed = 5;
    private float sprintSpeed = 15;

    private float horizontalInput;
    private float forwardInput;

    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale = new Vector3(1, 1f, 1);

    public CameraController cameraController;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraController == null)
        {
            cameraController = Camera.main.GetComponentInParent<CameraController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        // Check if sprint button is pressed
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        // Adjust speed based on sprinting
        float currentSpeed = isSprinting ? sprintSpeed : normalSpeed;

        Vector3 forward = cameraController.transform.forward;
        forward.y = 0f;
        forward.Normalize();
        Vector3 right = cameraController.transform.right;
        right.y = 0f;
        right.Normalize();

        // Calculate the movement direction based on camera rotation
        Vector3 moveDirection = forward * forwardInput + right * horizontalInput;
        // Moves the player based on input and camera rotation
        transform.Translate(moveDirection * Time.deltaTime * currentSpeed);

        // Check if crouch button is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localScale = crouchScale;
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        // Check if crouch button isn't pressed
        if (Input.GetKeyUp(KeyCode.Space))
        {
            transform.localScale = playerScale;
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        }

    }
}
